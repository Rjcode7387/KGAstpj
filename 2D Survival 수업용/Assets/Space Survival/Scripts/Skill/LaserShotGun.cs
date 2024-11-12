using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotGun : LaserGun
{
    public Transform[] shotPoints;

    protected override void Update()
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

      
    }

    protected override IEnumerator FireCoroutine()
    {
        while (true)
        {
            
            for (int i = 0; i < projectileCount; i++)
            {
                yield return new WaitForSeconds(shotInterval);
                Fire();
            }
            
        }
    }

    protected override void Fire()
    {
        foreach (Transform shotPonit in shotPoints)
        {
            Projectile proj =
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            //
            //proPool.Pop();
            LeanPool.Spawn(projectilePrefab, transform.position, Quaternion.identity);

            proj.transform.position = transform.position;

            proj.damage = damage;
            proj.moveSpeed = projectileSpeed;
            proj.transform.localScale *= projectileScale;
            proj.transform.up = shotPonit.position - transform.position;
            proj.pierceCount = pierceCount;

            
            LeanPool.Despawn(proj, proj.duration);
        }
        audioSource.PlayOneShot(fireaAudioClip);
    }
}
