using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HealItem : Item
{
    public ParticleSystem healparticle;
    public float healAmount;
    public override void Contact()
    {
        print("회복약 야미");
        GameManager.Instance.player.TakeHeal(healAmount);
            
        base.Contact();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var contactPoint = collision.transform.position;
            var particle = Instantiate(healparticle, contactPoint, Quaternion.identity);
            Destroy(particle.gameObject, 1f);
        }


    }

}
