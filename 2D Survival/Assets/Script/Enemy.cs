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

    private void Start()
    {
        GameManager.Instance.enemies.Add(this);
        target = GameManager.Instance.player.transform;
        maxHp = hp;
        
    }

    private void Update()
    {
        Vector2 moveDir = target.position - transform.position;
        Move(moveDir.normalized);
        //print(moveDir.magnitude);//vector.magnitude : 해당 백터가 "방향백터로" 간주될때, 백터의 길이 
        //print(moveDir.normalized);//방향을 유지한채 길이가 1로 고정된 백터
        Hpbar.fillAmount = hpAmount;
    }


    private void Move(Vector2 dir)//dir 값이 커져도 1로 고정을 하곳 싶을경우.
    {
        //normalized 방향을 유지하채 길이가 1로 고정된 백터
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
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
    private void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.killcount++;
        Destroy(gameObject);
    }
    public float damageInterval;
    public float preDamageTime;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (Time.time >= preDamageTime)
            {
                player.hp -= damage;
                preDamageTime = Time.time + damageInterval;
                
            }
        }
    }

}
