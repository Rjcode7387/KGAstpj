using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 0;//레벨
    public int exp = 0;//경험치

    //현업에서 개발되는 대부분의 게임은
    //exp값을 빼지않음
    //계속 exp를 누적하는 대신에
    //현재 exp를 레벨로 환산하면 몇 레벨에 해당하는지 계산

    private int[] levelupSteps = {100,200,300,400};//최대 레벨 5까지
    private int currentMaxExp; //현재 레벨에서 레벨업 하기까지 필요한 경험치량.

    private float maxHp;
    public float hp = 100f; //체력
    public float damage = 5f; //공격력
    public float moveSpeed = 5f; //이동속도

    public Projectile projectilePrefab; //투사체 프리팹

    public float HpAmount { get => hp/maxHp; } //현재 체력 비율

    public int killcount = 0;

    public Text killcountText;
    public Image hpBarImage;
    public Text leveltext;
    public Text expText;

    private Transform moveDir;
    private Transform fireDir;

    private Rigidbody2D rb;

    public Animator tailfireAnimCtrl;
    public Animator BlinkfireAnimCtrl;


    private void Awake()
    {
        moveDir = transform.Find("MoveDir");
        fireDir = transform.Find("FireDir");
        rb = GetComponent<Rigidbody2D>();
        //BlinkfireAnimCtrl=GetComponent<Animator>();
    }


    void Start()
    {
        maxHp = hp; // 최대체력 지정
        currentMaxExp = levelupSteps[0];//최대 경험치

        leveltext.text = (level+1).ToString();
        expText.text = exp.ToString();
        GameManager.Instance.player = this;
        //리턴이 있는 함수를 호출할 때 , 리턴을 사용하지 않는다면
        //_ : 언더바는 무시항목으로 아예 반환을 위한 메모리를 점유하지 않고 함수만 호출할수 있다.
        _=StartCoroutine(FireCoroutine());
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y);

       

        tailfireAnimCtrl.SetBool("IsMoving", moveDir.magnitude > 0.4f);
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
        Move(moveDir);

        isFiring = targetEnemy != null;

        killcountText.text =killcount.ToString();
        hpBarImage.fillAmount = HpAmount;

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
    public void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.transform.up = fireDir.up;
        projectile.damage = damage;

    }
    public float fireInterval;
    public bool isFiring;//적이있으면 true 적이없으면 false 활성화 비활성화 느낌으로 만들어보기

    //자동으로 투사체를 발사하는 코루틴
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);
            if(isFiring)Fire();
        }
    }

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

        BlinkfireAnimCtrl.SetTrigger("GetAttack");
        if (damage < 0)
        {
            
            damage = 0;
        }
        hp-=damage;
        if (hp <= 0)//게임오버처리
        {
            hp=0;
        }
    }
    //경험치 습득마다 호출
    public void GaniExp(int exp)
    {
        this.exp += exp;//습득한 경험치 더해줌
        if (level < levelupSteps.Length && this.exp >= currentMaxExp)
        {
           
            level++;
            this.exp =currentMaxExp;
            if(level < levelupSteps.Length) currentMaxExp = levelupSteps[level];
            
            
        }



        leveltext.text = (level+1).ToString();
        expText.text = this.exp.ToString();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
   
        //2.만약 특정 클래스를 상속하지 않고, 공통점이 없는 여러 객체들이 경우에따라
        //같은 행동을 해야 할경우. Interface를 사용할 수있음.

        if (collision.TryGetComponent<Item>(out var contact))
        {
            contact.Contact();
            
        }
       


    }

    

}
