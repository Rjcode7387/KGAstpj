using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 0;//����
    public int exp = 0;//����ġ

    //�������� ���ߵǴ� ��κ��� ������
    //exp���� ��������
    //��� exp�� �����ϴ� ��ſ�
    //���� exp�� ������ ȯ���ϸ� �� ������ �ش��ϴ��� ���

    private int[] levelupSteps = {100,200,300,400};//�ִ� ���� 5����
    private int currentMaxExp; //���� �������� ������ �ϱ���� �ʿ��� ����ġ��.

    private float maxHp;
    public float hp = 100f; //ü��
    public float damage = 5f; //���ݷ�
    public float moveSpeed = 5f; //�̵��ӵ�

    public Projectile projectilePrefab; //����ü ������

    public float HpAmount { get => hp/maxHp; } //���� ü�� ����

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
        maxHp = hp; // �ִ�ü�� ����
        currentMaxExp = levelupSteps[0];//�ִ� ����ġ

        leveltext.text = (level+1).ToString();
        expText.text = exp.ToString();
        GameManager.Instance.player = this;
        //������ �ִ� �Լ��� ȣ���� �� , ������ ������� �ʴ´ٸ�
        //_ : ����ٴ� �����׸����� �ƿ� ��ȯ�� ���� �޸𸮸� �������� �ʰ� �Լ��� ȣ���Ҽ� �ִ�.
        _=StartCoroutine(FireCoroutine());
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y);

       

        tailfireAnimCtrl.SetBool("IsMoving", moveDir.magnitude > 0.4f);
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
    public void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.transform.up = fireDir.up;
        projectile.damage = damage;

    }
    public float fireInterval;
    public bool isFiring;//���������� true ���̾����� false Ȱ��ȭ ��Ȱ��ȭ �������� ������

    //�ڵ����� ����ü�� �߻��ϴ� �ڷ�ƾ
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
        if (hp <= 0)//���ӿ���ó��
        {
            hp=0;
        }
    }
    //����ġ ���渶�� ȣ��
    public void GaniExp(int exp)
    {
        this.exp += exp;//������ ����ġ ������
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
   
        //2.���� Ư�� Ŭ������ ������� �ʰ�, �������� ���� ���� ��ü���� ��쿡����
        //���� �ൿ�� �ؾ� �Ұ��. Interface�� ����� ������.

        if (collision.TryGetComponent<Item>(out var contact))
        {
            contact.Contact();
            
        }
       


    }

    

}
