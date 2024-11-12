using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletspeed = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "borderBullet")
        {
            Destroy(gameObject);
            
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        transform.Translate(Vector2.up*bulletspeed*Time.deltaTime);
    }
}
