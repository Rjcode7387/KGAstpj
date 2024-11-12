using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public float cooldownTime = 5f; // 스킬 쿨다운 시간
    public float range = 5f; // 적 탐지 범위
    public ParticleSystem overLapParticle; // 스킬 효과 프리팹
    public GameObject projectilePrefab; // 발사체 프리팹

    private bool isCooldown = false;
    private List<Collider2D> hitColliders = new List<Collider2D>(10); // 초기 용량 설정

    void Start()
    {
        StartCoroutine(DECoroutine());
    }

    private IEnumerator DECoroutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                DEAndUseSkill();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void DEAndUseSkill()
    {
        hitColliders.Clear();
        hitColliders.AddRange(Physics2D.OverlapCircleAll(transform.position, range));

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                UseSkill(hitCollider.transform.position);
                break;
            }
        }
    }

    private void UseSkill(Vector3 position)
    {
        StartCooldown();

        if (overLapParticle != null)
        {
            var particle = Instantiate(overLapParticle, position, Quaternion.identity);
            Destroy(particle.gameObject, particle.main.duration);
        }

        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, position, Quaternion.identity);
        }
    }

    private void StartCooldown()
    {
        isCooldown = true;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}