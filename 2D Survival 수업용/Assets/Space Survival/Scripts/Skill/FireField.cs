using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireField : MonoBehaviour
{
    public float damage = 5f; // 장판 피해량
    public float duration = 5f; // 장판 지속 시간
    public float damageInterval = 1f; // 피해 간격
    public ParticleSystem areaParticle; // 파티클

    private void Start()
    {
        if (areaParticle != null)
        {
            var particle = Instantiate(areaParticle, transform.position, Quaternion.identity);
            particle.transform.SetParent(transform); 
        }

        StartCoroutine(DamageCoroutine());
        Destroy(gameObject, duration); // 장판 지속 시간 후 제거
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
