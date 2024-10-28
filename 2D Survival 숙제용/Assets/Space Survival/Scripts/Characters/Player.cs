using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Player : MonoBehaviour    
{
    public PlayerDataSO playerdata;

    public int level = 0;//����
    public int exp = 0;//����ġ  

    //�������� ���ߵǴ� ��κ��� ������
    //exp���� ��������
    //��� exp�� �����ϴ� ��ſ�
    //���� exp�� ������ ȯ���ϸ� �� ������ �ش��ϴ��� ���

    private int[] levelupSteps = {1000,3000,6000,10000};//�ִ� ���� 5����
    private int currentMaxExp; //���� �������� ������ �ϱ���� �ʿ��� ����ġ��.

    private float maxHp;
    public float hp = 100f; //ü��
    public float damage = 5f; //���ݷ�
    public float moveSpeed = 5f; //�̵��ӵ�

    //public Projectile projectilePrefab; //����ü ������

    public float HpAmount { get => hp/maxHp; } //���� ü�� ����

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
    

    //public float healthRegenRate = 1f; // �ʴ� ȸ����
    //public float regenInterval = 5f; // ȸ�� ����

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

        //GameObject Ȱ��ȭ / ��Ȱ��ȭ : SetActive(bool)
        //Component  Ȱ��ȭ / ��Ȱ��ȭ : enabled = bool;
        //�� ��� ��� OnEnabled/OnDisabled �޼��� �Լ��� ȣ��.
        //�������鶧�� ���� �ϵ���
        renderer.GetComponent<Rotater>().enabled = playerdata.rotateRenderer;


        maxHp = hp; // �ִ�ü�� ����
        currentMaxExp = levelupSteps[0];//�ִ� ����ġ
        //StartCoroutine(AutoRegenerateHealth());

        UIManager.Instance.levelText.text = (level + 1).ToString();
        UIManager.Instance.expText.text = exp.ToString();
        GameManager.Instance.player = this;
        //������ �ִ� �Լ��� ȣ���� �� , ������ ������� �ʴ´ٸ�
        //_ : ����ٴ� �����׸����� �ƿ� ��ȯ�� ���� �޸𸮸� �������� �ʰ� �Լ��� ȣ���Ҽ� �ִ�.
        //_=StartCoroutine(FireCoroutine());
        foreach (Skill skill in skills)
        {
            GameObject skillObj = Instantiate(skill.skillPrefabs[skill.skillLevel], transform, false); // ��ų ������Ʈ ����
            skillObj.name = skill.skillName;                                        // ������Ʈ �̸� ����
            skillObj.transform.localPosition = Vector2.zero;                        // ��ų ��ġ�� �÷��̾��� ��ġ�� ������
            if (skill.isTargetting)
            {
                skillObj.transform.SetParent(fireDir);  // �׻� ���� ���ϴ� ������Ʈ �ڽ����� ����
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

        

        Enemy targetEnemy = null;//������� ������ ��
        float targetDistance = float.MaxValue; //������ �Ÿ�


        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < targetDistance)
            {
                //������ ���� ������ ������
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
    /// Transform�� ���� ���� ������Ʈ�� �����̴� �Լ�.
    /// </summary>
    /// <param name="dir">�̵� ����</param>
    public void Move(Vector2 dir)
    {
        
        Vector2 movePos = rb.position +(dir*moveSpeed*Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }

    /// <summary>
    /// ����ü�� �߻�.
    /// </summary>
    //public void Fire()
    //{
    //    Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    //    projectile.transform.up = fireDir.up;
    //    projectile.damage = damage;

    //}
    //public float fireInterval;
    //public bool isFiring;//���������� true ���̾����� false Ȱ��ȭ ��Ȱ��ȭ �������� ������

    //�ڵ����� ����ü�� �߻��ϴ� �ڷ�ƾ
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
        if (hp <= 0)//���ӿ���ó��
        {
            hp=0;
            //TODO:���ӿ���ó��
            GameManager.Instance.GameOver();
        }
    }
    //����ġ ���渶�� ȣ��
    public void GainExp(int exp)
    {
        this.exp += exp;//������ ����ġ ������
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
            // ��ȿ���� ���� ��ų�� �Ѿ�Դ�.
            Debug.LogWarning($"�ִ� ������ ������ ��ų ���� ���� �õ���{skill.skillName}");
            return;
        }
        skill.skillLevel++;                 // ��ų ���� ���
        Destroy(skill.currentSkillObject);  // ������ �ִ� ��ų ������Ʈ�� ����
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
