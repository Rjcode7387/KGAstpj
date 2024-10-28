using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Player : MonoBehaviour    
{
    public PlayerDataSO playerdata;

    public int level = 0;//레벨
    public int exp = 0;//경험치  

    //현업에서 개발되는 대부분의 게임은
    //exp값을 빼지않음
    //계속 exp를 누적하는 대신에
    //현재 exp를 레벨로 환산하면 몇 레벨에 해당하는지 계산

    private int[] levelupSteps = {1000,3000,6000,10000};//최대 레벨 5까지
    private int currentMaxExp; //현재 레벨에서 레벨업 하기까지 필요한 경험치량.

    private float maxHp;
    public float hp = 100f; //체력
    public float damage = 5f; //공격력
    public float moveSpeed = 5f; //이동속도

    //public Projectile projectilePrefab; //투사체 프리팹

    public float HpAmount { get => hp/maxHp; } //현재 체력 비율

    public int killCount = 0;
    public int totalKillCount = 0;

    //public Text killcountText;
    //public Image hpBarImage;
    //public Text leveltext;
    //public Text expText;

    private Transform moveDir;
    private Transform fireDir;

    private Rigidbody2D rb;

    public new SpriteRenderer renderer;

    public Animator anim;
    

    //public float healthRegenRate = 1f; // 초당 회복량
    //public float regenInterval = 5f; // 회복 간격

    public List<Skill> skills;


    private void Awake()
    {
        moveDir = transform.Find("MoveDir");
        fireDir = transform.Find("FireDir");
        rb = GetComponent<Rigidbody2D>();
        //BlinkfireAnimCtrl=GetComponent<Animator>();
    }


    void Start()
    {
        hp = playerdata.hp;
        damage = playerdata.damage;
        moveSpeed = playerdata.movespeed;
        name = playerdata.charactername;
        renderer.sprite = playerdata.sprite;
        //Instantiate(playerdata.startSkillPrefab, transform, false);

        //GameObject 활성화 / 비활성화 : SetActive(bool)
        //Component  활성화 / 비활성화 : enabled = bool;
        //두 경우 모두 OnEnabled/OnDisabled 메세지 함수가 호출.
        //직접만들때는 주의 하도록
        renderer.GetComponent<Rotater>().enabled = playerdata.rotateRenderer;


        maxHp = hp; // 최대체력 지정
        currentMaxExp = levelupSteps[0];//최대 경험치
        //StartCoroutine(AutoRegenerateHealth());

        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = exp.ToString();
        GameManager.Instance.player = this;
        //리턴이 있는 함수를 호출할 때 , 리턴을 사용하지 않는다면
        //_ : 언더바는 무시항목으로 아예 반환을 위한 메모리를 점유하지 않고 함수만 호출할수 있다.
        //_=StartCoroutine(FireCoroutine());
        foreach (Skill skill in skills)
        {
            GameObject skillObj = Instantiate(skill.skillPrefabs[skill.skillLevel], transform, false); // 스킬 오브젝트 생성
            skillObj.name = skill.skillName;                                        // 오브젝트 이름 변경
            skillObj.transform.localPosition = Vector2.zero;                        // 스킬 위치를 플레이어의 위치로 가져옴
            if (skill.isTargetting)
            {
                skillObj.transform.SetParent(fireDir);  // 항상 적을 향하는 오브젝트 자식으로 만듦
            }
            skill.currentSkillObject = skillObj;
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y);

       

        anim.SetBool("IsMoving", moveDir.magnitude > 0.4f);
        //tailfireAnimCtrl.SetBool()

        

        Enemy targetEnemy = null;//대상으로 지정된 적
        float targetDistance = float.MaxValue; //대상과의 거리


        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < targetDistance)
            {
                //이전에 비교한 적보다 가까우면
                targetDistance = distance;
                targetEnemy = enemy;
            }
        }
        Vector2 fireDir = Vector2.zero;
        if (targetEnemy != null)
        {
            fireDir = targetEnemy.transform.position - transform.position;


        }
        

        

        UIManager.Instance.killCountText.text = killCount.ToString();
        UIManager.Instance.totalKillCountText.text = totalKillCount.ToString();
        UIManager.Instance.hpBarImage.fillAmount = HpAmount;
        Move(moveDir);
        if (moveDir.magnitude > 0.1f)
        {
            this.moveDir.up =moveDir;
        }
        this.fireDir.up = fireDir;    

    }

    /// <summary>
    /// Transform을 통해 게임 오브젝트를 움직이는 함수.
    /// </summary>
    /// <param name="dir">이동 방향</param>
    public void Move(Vector2 dir)
    {
        
        Vector2 movePos = rb.position +(dir*moveSpeed*Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }

    /// <summary>
    /// 투사체를 발사.
    /// </summary>
    //public void Fire()
    //{
    //    Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    //    projectile.transform.up = fireDir.up;
    //    projectile.damage = damage;

    //}
    //public float fireInterval;
    //public bool isFiring;//적이있으면 true 적이없으면 false 활성화 비활성화 느낌으로 만들어보기

    //자동으로 투사체를 발사하는 코루틴
    //private IEnumerator FireCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(fireInterval);
    //        if (isFiring)Fire();
    //    }
    //}

    public void TakeHeal(float heal)
    {
        hp += heal;
        if (hp > maxHp)
        {
            hp = maxHp;
        }

    }

    public void TakeDamage(float damage)
    {

        anim.SetTrigger("Hit");
        if (damage < 0)
        {
            
            damage = 0;
        }
        hp-=damage;
        if (hp <= 0)//게임오버처리
        {
            hp=0;
            //TODO:게임오버처리
            GameManager.Instance.GameOver();
        }
    }
    //경험치 습득마다 호출
    public void GainExp(int exp)
    {
        this.exp += exp;//습득한 경험치 더해줌
        if (level < levelupSteps.Length && this.exp >= currentMaxExp)
        {
            OnLevelUp();
        }
        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = this.exp.ToString();
    }

    private void OnLevelUp()
    {
        level++;
        exp -= currentMaxExp;


        if (level < levelupSteps.Length)
        {
            currentMaxExp = levelupSteps[level];
            UIManager.Instance.levelupPanel.LevelUpPanelOpen(skills, OnSkillLevelUp);
        }

       
       
    }

    public void OnSkillLevelUp(Skill skill)
    {
        if (skill.skillLevel >= skill.skillPrefabs.Length - 1)
        {
            // 유효하지 않은 스킬이 넘어왔다.
            Debug.LogWarning($"최대 레벨에 도달한 스킬 레벨 업을 시도함{skill.skillName}");
            return;
        }
        skill.skillLevel++;                 // 스킬 레벨 상승
        Destroy(skill.currentSkillObject);  // 기존에 있던 스킬 오브젝트를 제거
        skill.currentSkillObject = Instantiate(skill.skillPrefabs[skill.skillLevel], transform, false);
        skill.currentSkillObject.name = skill.skillPrefabs[skill.skillLevel].name;
        skill.currentSkillObject.transform.localPosition = Vector2.zero;
        if (skill.isTargetting)
        {
            skill.currentSkillObject.transform.SetParent(fireDir);
        }
    }
    //private IEnumerator AutoRegenerateHealth()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(regenInterval);
    //        TakeHeal(healthRegenRate);
    //    }
    //}

}
