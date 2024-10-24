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

    public float maxDist;//�ִ� Ÿ�ٰŸ�




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
        //���� Vector2 �������� ���ؼ� ����ü�� ����

        Vector2 pos = Random.insideUnitCircle * maxDist;


        //�����غ���
        //���� ��ġ�� �� ������Ʈ ���� �� transform�� �������� ���� �ۼ�
        //����ź 

        MissileProjectile proj = Instantiate(projectilePrefab, pos, Quaternion.identity);

        proj.damage = damage;

        proj.duration = 1/ projectileSpeed;
    }


}
