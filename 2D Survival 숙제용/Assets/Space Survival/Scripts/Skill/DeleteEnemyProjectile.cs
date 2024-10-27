using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemyProjectile : MonoBehaviour
{
    public float damage = 10f; // �߻�ü ���ط�
    public float speed = 5f; // �߻�ü �ӵ�

    void Update()
    {
        // �߻�ü �̵�
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                print($"������{name}");
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // �浹 �� �߻�ü ����
        }
    }
}
