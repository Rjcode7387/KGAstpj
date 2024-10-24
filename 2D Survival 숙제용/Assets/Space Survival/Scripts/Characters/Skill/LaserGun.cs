using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Transform target; //����ü�� ���ؾ� �� ���⿡ �ִ� ���
    public Projectile projectilePrefab; // ����ü ������
    public ProjectilePool proPool; // projectile Prefab�� ������� ���� ������Ʈ�� �����ϴ� ������ƮǮ.

    public float damage; //������
    public float projectileSpeed; // ����ü �ӵ�
    public float projectileScale; // ����ü ũ��
    public float shotInterval; //���� ����
    //�߰�
    public bool isFiring;//���� ������ true ������ false Ȱ��ȭ ��Ȱ��ȭ   
    public int projectileCount; // ����ü ���� 1~5
    public float innerInterval;
    [Tooltip("���� Ƚ���Դϴ� \n������ ������ ���� ��� 0�Է�")]
    public int pierceCount; // ����Ƚ��
   

   
    
    protected virtual void Start()
    {
        StartCoroutine(FireCoroutine());
    }
    protected virtual void Update()
    {
        if (target == null) return;
        transform.up = target.position - transform.position;
        Enemy targetEnemy = null;//������� ������ ��
        float targetDistance = float.MaxValue; //������ �Ÿ�


        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < targetDistance)
            {
                //������ ���� ������ ������
                targetDistance = distance;
                targetEnemy = enemy;
            }
        }
        Vector2 fireDir = Vector2.zero;
        if (targetEnemy != null)
        {
            fireDir = targetEnemy.transform.position - transform.position;

        }
        isFiring = targetEnemy != null;
    }

    protected virtual IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            //1. ����ü ������ �ö󰡸� 0.05�� �������� ����ü ������ŭ �߻� �ݺ�
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
        proPool.Pop();
        proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

        proj.damage = damage;
        proj.moveSpeed = projectileSpeed;
        proj.transform.localScale *= projectileScale;
        proj.pierceCount = pierceCount;
        
        

    }

}
