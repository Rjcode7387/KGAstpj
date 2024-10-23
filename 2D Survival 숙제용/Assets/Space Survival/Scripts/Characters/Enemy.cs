using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
    
    private float maxHp;
    public float hp = 10f; //체력
    public float damage = 10f; //공격력
    public float moveSpeed = 3f; //이동 속도

    
    public float hpAmount { get { return hp / maxHp; } } //자주 계산되는 항목은 프로퍼티로 만들기
    

    private Transform target; 

    public Image hpBar;

    private Rigidbody2D rb;

    public ParticleSystem impactParticle;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()//스타트 메세지 함수는 코루틴이 될 수있다
    {
        GameManager.Instance.enemies.Add(this); //적 리스트에 자기 자신을 Add
        yield return null;//한프레임 쉬기
        target = GameManager.Instance.player.transform;
        maxHp = hp;
    }

    private void Update()
    {
        if (target == null) return;

        //Vector2 moveDir = target  != null ? target.position - transform.position : Vector2.one;
        //?, ?? null check를 하는 접근 연산자
        Vector2 moveDir = target?.position - transform.position ?? Vector2.zero;
        Move(moveDir.normalized);
        
        hpBar.fillAmount = hpAmount;
    }

    public void Move(Vector2 dir)//dir 값이 커져도 1로 고정을 하고 싶을경우=>normalized
    {
      

        Vector2 movePos = rb.position+(dir * moveSpeed*Time.fixedDeltaTime);
        
        rb.MovePosition(movePos);

    }

    //OnHit,
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0) 
        {
            Die();
        }

    }

    int exp = 100;
    public void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.killcount++;
        GameManager.Instance.player.GaniExp(exp);
        Destroy(gameObject);
    }

    public float damageInterval;//데미지 간격
    private float preDamageTime;// 이전에 데미지를 준 시간(Time.time)

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (preDamageTime + damageInterval > Time.time)
        {
            return;
        }

        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(damage);
            var particle = Instantiate(impactParticle, collision.GetContact(0).point, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, 2f);
            preDamageTime = Time.time;
        }
    }


   
}
