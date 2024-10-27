using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Transform target; //투사체가 향해야 할 방향에 있는 대상
    public Projectile projectilePrefab; // 투사체 프리펩
    public ProjectilePool proPool; // projectile Prefab로 만들어진 게임 오브젝트를 관리하는 오브젝트풀.

    public float damage; //데미지
    public float projectileSpeed; // 투사체 속도
    public float projectileScale; // 투사체 크기
    public float shotInterval; //공격 간격
    //추가
    public bool isFiring;//적이 있으면 true 없으면 false 활성화 비활성화   
    public int projectileCount; // 투사체 개수 1~5
    public float innerInterval;
    [Tooltip("관통 횟수입니다 \n무제한 관통을 원할 경우 0입력")]
    public int pierceCount; // 관통횟수


   


    protected virtual void Start()
    {
        StartCoroutine(FireCoroutine());
    }
    protected virtual void Update()
    {
        Enemy targetEnemy = null;
        float targetDistance = float.MaxValue;

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < targetDistance)
            {
                targetDistance = distance;
                targetEnemy = enemy;
            }
        }

        if (targetEnemy != null)
        {
            target = targetEnemy.transform;
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }

        if (target != null)
        {
            transform.up = target.position - transform.position;
        }
    }

    protected virtual IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            //1. 투사체 개수가 올라가면 0.05초 간격으로 투사체 개수만큼 발사 반복
            for (int i = 0; i < projectileCount; i++)
            {
                yield return new WaitForSeconds(innerInterval);
                if (isFiring)Fire();
                
            }
        }
    }

    protected virtual void Fire()
    {
        Projectile proj =
        //Instantiate(projectilePrefab,transform.position, transform.rotation);
        //proPool.Pop();
        //proj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);

        proj.damage = damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale *= projectileScale;
        proj.pierceCount = pierceCount;

        LeanPool.Despawn(proj, proj.duration);



    }

}
