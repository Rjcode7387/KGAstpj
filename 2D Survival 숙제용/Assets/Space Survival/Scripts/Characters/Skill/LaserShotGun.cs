using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotGun : LaserGun
{
    public Transform[] shotPoints;


    protected override IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    protected override void Fire()
    {
        foreach (Transform shotPonit in shotPoints)
        {
            Projectile proj =
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proPool.Pop();
            proj.transform.position = transform.position;

            proj.damage = damage;
            proj.moveSpeed = projectileSpeed;
            proj.transform.localScale *= projectileScale;
            proj.transform.up = shotPonit.position - transform.position;
            proj.pierceCount = pierceCount;
        }
    }
}
