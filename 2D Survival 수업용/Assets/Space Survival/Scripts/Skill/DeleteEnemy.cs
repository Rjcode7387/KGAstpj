using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    public float cooldownTime = 5f; // ��ų ��ٿ� �ð�
    public float range = 5f; // �� Ž�� ����
    public ParticleSystem overLapParticle; // ��ų ȿ�� ������
    public GameObject projectilePrefab; // �߻�ü ������

    private bool isCooldown = false;
    private List<Collider2D> hitColliders = new List<Collider2D>(10); // �ʱ� �뷮 ����

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