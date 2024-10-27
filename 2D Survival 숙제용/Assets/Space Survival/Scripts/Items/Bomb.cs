using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public ParticleSystem BombParticle;
   
    public override void Contact()
    {
        print("ÆøÅº ¾ß¹Ì");      
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
