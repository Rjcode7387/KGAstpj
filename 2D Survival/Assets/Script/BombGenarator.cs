using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenarator : MonoBehaviour
{
    public GameObject prefabsBomb;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public int creatbomb;
    public float Interval = 5f;

    private void Start()
    {
        StartCoroutine(BombSpawn());
        print($"�����ȴ�{name}");
    }

    private IEnumerator BombSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Interval);

            Spawn();
        }

    }
    private void Spawn()
    {
        for (int i = 0; i < creatbomb; i++)
        {
            // ���� ��ġ ���
            Vector3 randomPosition = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z)
            );

            // �ν��Ͻ� ����
            Instantiate(prefabsBomb, randomPosition, Quaternion.identity);
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
