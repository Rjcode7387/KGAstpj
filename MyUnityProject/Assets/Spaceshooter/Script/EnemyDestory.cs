using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boxborder")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);    
        }

    }
}
