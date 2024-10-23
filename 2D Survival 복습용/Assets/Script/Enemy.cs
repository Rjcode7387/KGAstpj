using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    //public float maxHP = 10f;//하수
    private float maxHp;
    public float hp = 10f;//체력
    public float damage = 10f;//공격력
    public float moveSpeed = 3f;//이동속도

    //초고수
    public float hpAmount {  get { return hp/maxHp; } }//자주 계산되는 항목은 프로퍼티로 만들기
    //Getter/Setter


    private Transform target;//추적대상
    public Image Hpbar;

    private Rigidbody2D rb;
    public ParticleSystem impactParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }


    private IEnumerator Start()
    {
        GameManager.Instance.enemies.Add(this); //적 리스트에 자기 자신을 Add
        yield return null;//한프레임 쉬기
        target = GameManager.Instance.player.transform;
        maxHp = hp;

    }

    private void Update()
    {
        if (target == null) return;
        Vector2 moveDir = target?.position - transform.position?? Vector2.zero;
        Move(moveDir.normalized);
        //print(moveDir.magnitude);//vector.magnitude : 해당 백터가 "방향백터로" 간주될때, 백터의 길이 
        //print(moveDir.normalized);//방향을 유지한채 길이가 1로 고정된 백터
        Hpbar.fillAmount = hpAmount;
    }


    private void Move(Vector2 dir)//dir 값이 커져도 1로 고정을 하곳 싶을경우.
    {
        Vector2 movePos = rb.position+(dir * moveSpeed*Time.fixedDeltaTime);
        //normalized 방향을 유지하채 길이가 1로 고정된 백터
        rb.MovePosition(movePos);
    }

    //Onhit
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)//쥬금
        {
            Die();

        }
    }
    int exp = 50;
    private void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.killcount++;
        GameManager.Instance.player.GaniExp(exp);
        Destroy(gameObject);
    }
    public float damageInterval;
    public float preDamageTime;
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
