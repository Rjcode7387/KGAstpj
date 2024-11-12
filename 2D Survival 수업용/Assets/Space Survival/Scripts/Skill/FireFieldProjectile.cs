using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFieldProjectile : MonoBehaviour
{

    public float range = 5f; // 장판 생성 범위
    public GameObject areaPrefab; // 장판 프리팹
    public int numberOfFields = 3; // 생성할 장판의 수
    public float cooldownTime = 5f; // 쿨타임

    private bool isCooldown = false;
    private WaitForSeconds cooldownWait; // 쿨타임 대기 객체
    private WaitForSeconds checkInterval = new WaitForSeconds(1f); // 체크 간격

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
            yield return checkInterval; // 1초마다 체크
        }
    }

    private void CreateRandomFields()
    {
        if (areaPrefab == null) return; // areaPrefab이 null이면 실행하지 않음

        for (int i = 0; i < numberOfFields; i++)
        {
            Vector2 randomPosition = RandomPosition();
            Instantiate(areaPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector2 RandomPosition()
    {
        // 범위 내 랜덤한 위치 계산
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
