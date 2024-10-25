using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
   public static ProjectilePool pool;
    public Projectile projPrefab;


    private void Awake()
    {
        pool = this;
    }

    List<Projectile> poolList = new();

    public Projectile Pop()
    {
        if (poolList.Count <= 0) //²¨³¾ °´Ã¼ ¾øÀ½
            Push(Instantiate(projPrefab));

        Projectile proj = poolList[0];

        poolList.Remove(proj);

        proj.gameObject.SetActive(true);
        proj.transform.SetParent(null);

        return proj;
    }
    public void Push(Projectile proj)
    {
        poolList.Add(proj);
        proj.gameObject.SetActive(false);
        proj.transform.SetParent(transform, false);
    }
    public void Push(Projectile proj, float delay)
    {
        StartCoroutine(PushCoroutine(proj, delay));
    }
    IEnumerator PushCoroutine(Projectile proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(proj);
    }
}
