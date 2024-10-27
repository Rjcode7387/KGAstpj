using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemyProjectile : MonoBehaviour
{
    public float damage = 10f; // 발사체 피해량
    public float speed = 5f; // 발사체 속도

    void Update()
    {
        // 발사체 이동
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                print($"숔웨입{name}");
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // 충돌 후 발사체 제거
        }
    }
}
