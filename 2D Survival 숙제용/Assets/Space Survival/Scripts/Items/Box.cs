using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Item
{
   public ParticleSystem BoxParticle;
   public GameObject item;
    public override void Contact()
    {
        print($" »óÀÚ¶û Ãæµ¹");
        Instantiate(item,transform.position, Quaternion.identity);
        base.Contact();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var contactPoint = collision.transform.position;
            var particle = Instantiate(BoxParticle, contactPoint, Quaternion.identity);
            Destroy(particle.gameObject, 2f);
        }
    }


}
