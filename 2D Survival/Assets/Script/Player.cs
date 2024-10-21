using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int killcount;
    private float playermaxhp;
    public float hp=100f;//ü��
    public float damage=10f;//���ݷ�
    public float moveSpeed=5f;//�̵��ӵ�
    private Enemy enemy;
    
    public float PhpAmount { get { return hp/playermaxhp; } }

    public Projectie projectilePrefab;//����ü ������
    public Image Hpbar;

    private void Start()
    {
        playermaxhp = hp;
    }




    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Hpbar.fillAmount = PhpAmount;
        Vector2 dir = new Vector2(x, y);
        
        Move(dir);

        if (Input.GetButtonDown("Fire1"))
        {
            //vector3�� vector2�� ĳ���� �Ҷ� : z���� �����ȴ�.
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 firdir = mouseScreenPos - (Vector2)transform.position;

            Fire(firdir);
        }
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
        }
    }
    //public void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        print($"{collision.gameObject.name}");
    //        enemy = collision.gameObject.GetComponent<Enemy>();
    //        PlayerTakeDamage(hp - enemy.damage);

    //    }

    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        print($"{collision.gameObject.name}");
    //        enemy = collision.gameObject.GetComponent<Enemy>();
    //        PlayerTakeDamage(hp - enemy.damage);

    //    }
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            print("�浹 !! ");
            PlayerTakeDamage(enemy.damage);
            print(hp);
        }
    }
    
}
