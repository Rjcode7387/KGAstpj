using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int killcount = 0;
    private float playermaxhp;
    public float hp=100f;//ü��
    public float damage=10f;//���ݷ�
    public float moveSpeed=5f;//�̵��ӵ�
    private float nextDamageTime = 0f;
    private float CooldownDamage = 2f;
    public TextMeshProUGUI Killtext;    
    public float PhpAmount { get { return hp/playermaxhp; } }
    public Projectie projectilePrefab;//����ü ������
    public Image Hpbar;
    private Transform moveDir;
    private Transform fireDir;
   

    private void Awake()
    {
        moveDir= transform.Find("MoveDir");
        fireDir= transform.Find("FireDir");
    }

    private void Start()
    {
        playermaxhp = hp;
        GameManager.Instance.player = this;
    }




    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Hpbar.fillAmount = PhpAmount;
        Killtext.text = $"Killcount : {killcount}";
        Vector2 moveDir = new Vector2(x, y);
        
        

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
        if (Input.GetButtonDown("Fire1"))
        {            
            Fire(fireDir);
        }
        

        this.moveDir.up = moveDir;

        this.fireDir.up = fireDir;
    }
    /// <summary>
    /// Transform�� ���� ���� ������Ʈ�� �����̴� �Լ�
    /// </summary>
    /// <param name="dir">�̵�����</param>
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);     
    }
    /// <summary>
    /// ����ü �߻�
    /// </summary>
    public void Fire(Vector2 dir)
    {
        Projectie projecttile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);

        projecttile.transform.up =dir;
        projecttile.damage = damage;
        //projecttile.moveSpeed = moveSpeed;�нú�� ������ �ӵ�
    }
    public void PlayerTakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)//���
        {
            Destroy(gameObject);
            //���� ����ó��
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {

        //if (other.TryGetComponent<Enemy>(out Enemy enemy))
        //{
        //    print("�浹 !! ");
        //    PlayerTakeDamage(enemy.damage);
        //    print(hp);
        //}
        if(other.TryGetComponent<Healitem>(out Healitem healitem))
        {
            print("�� ������ ����!!");
            GameManager.Instance.HealPlayer(healitem.healAmount); // GameManager�� ���� ȸ��
            Destroy(healitem.gameObject); // �� 
        }
    }
   


    public void Heal(float amount)
    {
        hp += amount;
        if (hp > playermaxhp)
        {
            hp = playermaxhp; // �ִ� ü���� �ʰ����� �ʵ��� ����
        }

        Debug.Log("��! ���� ü��: " + hp);
    }

}


