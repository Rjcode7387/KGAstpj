using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int killcount;
    private float playermaxhp;
    public float hp=100f;//체력
    public float damage=10f;//공격력
    public float moveSpeed=5f;//이동속도
    private Enemy enemy;
    
    public float PhpAmount { get { return hp/playermaxhp; } }

    public Projectie projectilePrefab;//투사체 프리펩
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
            //vector3를 vector2로 캐스팅 할때 : z값이 생략된다.
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 firdir = mouseScreenPos - (Vector2)transform.position;

            Fire(firdir);
        }
    }
    /// <summary>
    /// Transform을 통해 게임 오브젝트를 움직이는 함수
    /// </summary>
    /// <param name="dir">이동방향</param>
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);     
    }
    /// <summary>
    /// 투사체 발사
    /// </summary>
    public void Fire(Vector2 dir)
    {
        Projectie projecttile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);

        projecttile.transform.up =dir;
        projecttile.damage = damage;
        //projecttile.moveSpeed = moveSpeed;패시브로 증가된 속도
    }
    public void PlayerTakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)//쥬금
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
            print("충돌 !! ");
            PlayerTakeDamage(enemy.damage);
            print(hp);
        }
    }
    
}
