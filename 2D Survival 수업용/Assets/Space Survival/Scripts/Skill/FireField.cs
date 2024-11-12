using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireField : MonoBehaviour
{
    public float damage = 5f; // ���� ���ط�
    public float duration = 5f; // ���� ���� �ð�
    public float damageInterval = 1f; // ���� ����
    public ParticleSystem areaParticle; // ��ƼŬ

    private void Start()
    {
        if (areaParticle != null)
        {
            var particle = Instantiate(areaParticle, transform.position, Quaternion.identity);
            particle.transform.SetParent(transform); 
        }

        StartCoroutine(DamageCoroutine());
        Destroy(gameObject, duration); // ���� ���� �ð� �� ����
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);
            DealDamage();
        }
    }

    private void DealDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                var enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
