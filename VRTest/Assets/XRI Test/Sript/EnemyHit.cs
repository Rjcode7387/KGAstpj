using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameShoot game;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            game.AddScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
