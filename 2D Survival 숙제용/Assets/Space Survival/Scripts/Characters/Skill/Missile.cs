using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Missile : MonoBehaviour
{
    public Transform target;   

    public MissileProjectile projectilePrefab;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    public float maxDist;//최대 타겟거리




    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }
    private void Fire()
    {
        //랜덤 Vector2 포지션을 정해서 투사체를 생성

        Vector2 pos = Random.insideUnitCircle * maxDist;


        //직접해보기
        //랜덤 위치에 빈 오브젝트 생성 후 transform을 가져오는 로직 작성
        //유도탄 

        MissileProjectile proj = Instantiate(projectilePrefab, pos, Quaternion.identity);

        proj.damage = damage;

        proj.duration = 1/ projectileSpeed;
    }


}
