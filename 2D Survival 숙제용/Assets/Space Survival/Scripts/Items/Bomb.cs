using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public ParticleSystem BombParticle;
    //특덩 메세지함수가 없는 Component는 Enable/Disable이 동작하지 않음

    //private void Update()  
    //private void Start()
   
    private void Awake()
    {
        //enabled여부에 관계 없이 호출되는 메세지 함수
    }

    public override void Contact()
    {
        print("폭탄 야미");      
        GameManager.Instance.RemoveAllEnemies();
        base.Contact();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var contactPoint = collision.transform.position;
            var particle = Instantiate(BombParticle, contactPoint, Quaternion.identity);
            Destroy(particle.gameObject, 2f);

            Contact();
        }
    }
}
