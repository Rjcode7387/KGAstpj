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
    public float hp=100f;//체력
    public float damage=10f;//공격력
    public float moveSpeed=5f;//이동속도
    private float nextDamageTime = 0f;
    private float CooldownDamage = 2f;
    public TextMeshProUGUI Killtext;    
    public float PhpAmount { get { return hp/playermaxhp; } }
    public Projectie projectilePrefab;//투사체 프리펩
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
        if (Input.GetButtonDown("Fire1"))
        {            
            Fire(fireDir);
        }
        

        this.moveDir.up = moveDir;

        this.fireDir.up = fireDir;
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
            //게임 오버처리
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {

        //if (other.TryGetComponent<Enemy>(out Enemy enemy))
        //{
        //    print("충돌 !! ");
        //    PlayerTakeDamage(enemy.damage);
        //    print(hp);
        //}
        if(other.TryGetComponent<Healitem>(out Healitem healitem))
        {
            print("힐 아이템 먹음!!");
            GameManager.Instance.HealPlayer(healitem.healAmount); // GameManager를 통해 회복
            Destroy(healitem.gameObject); // 힐 
        }
    }
   


    public void Heal(float amount)
    {
        hp += amount;
        if (hp > playermaxhp)
        {
            hp = playermaxhp; // 최대 체력을 초과하지 않도록 제한
        }

        Debug.Log("힐! 현재 체력: " + hp);
    }

}


