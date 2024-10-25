
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ü
public class Projectile : MonoBehaviour
{
    public float damage = 10;//������
    public float moveSpeed = 5;//�̵��ӵ�
    public float duration = 3;//���ӽð�

    public int pierceCount = 0; // ����Ƚ��

    private CircleCollider2D coll;
    public ParticleSystem impactParticle;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = true;
    }
    private void OnEnable()
    {
        // Destroy(gameObject ,duration); //3�� �Ŀ� ������Ʈ ����
        ProjectilePool.pool.Push(this, duration);
    }

    List<Collider2D> contactedColls = new();    
    private void Update()
    {
        Move(Vector2.up);
        Collider2D contactedColl = Physics2D.OverlapCircle(transform.position, coll.radius);
        if (contactedColl != null)
        {
            if (contactedColl.CompareTag("Enemy"))
            {
                if (false == contactedColls.Contains(contactedColl))
                {
                    
                    print($"Contacted collider : {contactedColl.name}");
                    contactedColls.Add(contactedColl);


                    pierceCount--;
                    if (pierceCount == 0)
                    {
                        
                        //Destroy(gameObject);
                        ProjectilePool.pool.Push(this);
                    }
                }
            }

        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, coll.radius);
    //}

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            if (impactParticle != null)
            {
                ParticleSystem particle = Instantiate(impactParticle,transform.position,Quaternion.identity);
                particle.Play();
                Destroy(particle.gameObject, particle.main.duration);
            }

            //Destroy(gameObject);
            ProjectilePool.pool.Push(this);
           
        }
    }

    private void OnDisable()
    {
        contactedColls.Clear();
    }


}
