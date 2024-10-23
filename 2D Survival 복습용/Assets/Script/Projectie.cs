using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectie : MonoBehaviour
{
    public float damage = 10;//������
    public float moveSpeed = 5;//�̵��ӵ�
    public float duration=3;//���ӽð�

    private void Start()
    {
        Destroy(gameObject, duration); // 3�� �Ŀ� ������Ʈ ����
    }
    private void Update()
    {
        Move(Vector2.up);
    }
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    

   

}
