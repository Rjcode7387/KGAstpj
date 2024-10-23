using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

//����ü
public class Projectile : MonoBehaviour
{
    public float damage = 10;//������
    public float moveSpeed = 5;//�̵��ӵ�
    public float duration = 3;//���ӽð�

    public ParticleSystem impacParticle;

    private void Start()
    {
        Destroy(gameObject ,duration); //3�� �Ŀ� ������Ʈ ����
    }

    private void Update()
    {
        Move(Vector2.up);
        //Physics2D.OverlapCircle();
    }

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            var contactPoint = other.transform.position;
            var particle = Instantiate(impacParticle, contactPoint, Quaternion.identity);

            enemy.TakeDamage(damage);
            Destroy(particle.gameObject, 1f);
            Destroy(gameObject);
            
        }

    }

}
