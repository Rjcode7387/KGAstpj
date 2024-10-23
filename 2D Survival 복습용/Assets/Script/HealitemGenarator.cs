using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealitemGenarator : MonoBehaviour
{
    public GameObject prefabsHeal;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public int creatHeal;
    public float Interval = 5f;

    private void Start()
    {
        StartCoroutine(HealitemSpawn());
        print($"�����ȴ�{name}");
    }

    private IEnumerator HealitemSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Interval);

            Spawn();
        }

    }
    private void Spawn()
    {
        for (int i = 0; i < creatHeal; i++)
        {
            // ���� ��ġ ���
            Vector3 randomPosition = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z)
            );

            // �ν��Ͻ� ����
            Instantiate(prefabsHeal, randomPosition, Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
