using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFieldProjectile : MonoBehaviour
{

    public float range = 5f; // ���� ���� ����
    public GameObject areaPrefab; // ���� ������
    public int numberOfFields = 3; // ������ ������ ��
    public float cooldownTime = 5f; // ��Ÿ��

    private bool isCooldown = false;
    private WaitForSeconds cooldownWait; // ��Ÿ�� ��� ��ü
    private WaitForSeconds checkInterval = new WaitForSeconds(1f); // üũ ����

    private void Start()
    {
        cooldownWait = new WaitForSeconds(cooldownTime);
        StartCoroutine(FieldCreationCoroutine());
    }

    private IEnumerator FieldCreationCoroutine()
    {
        while (true)
        {
            if (!isCooldown)
            {
                CreateRandomFields();
                StartCooldown();
            }
            yield return checkInterval; // 1�ʸ��� üũ
        }
    }

    private void CreateRandomFields()
    {
        if (areaPrefab == null) return; // areaPrefab�� null�̸� �������� ����

        for (int i = 0; i < numberOfFields; i++)
        {
            Vector2 randomPosition = RandomPosition();
            Instantiate(areaPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector2 RandomPosition()
    {
        // ���� �� ������ ��ġ ���
        float randomX = Random.Range(-range, range);
        float randomY = Random.Range(-range, range);
        return new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }

    private void StartCooldown()
    {
        isCooldown = true;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return cooldownWait;
        isCooldown = false;
    }
}
