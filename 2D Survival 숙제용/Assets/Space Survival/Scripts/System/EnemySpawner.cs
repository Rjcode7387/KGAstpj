using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("�ѹ��� ������ ���� ��.\nX:�ּ�,Y:�ִ�")]
    public Vector2Int minMaxCount;
    [Tooltip("������ �� �÷��̾�κ����� �ִ�/�ּ� �Ÿ�\n x :�ּ�, y :�ִ�")]
    public Vector2Int minMaxDist;

    public GameObject enemyPrefab;
    public float spawnInterval; //��������

    private void Start()
    {
      
        StartCoroutine(SpawnCoroutine());
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(spawnInterval);
            int enemyCount = Random.Range(minMaxCount.x, minMaxCount.y);
            Spawn(enemyCount);    // ���� ����
        }
    }
    private void Spawn(int count)
    {
        Enemy enemy = EnemyPool.Enpool.Pop();
        enemy.transform.position = SpawnPosition();
    }

    private Vector2 SpawnPosition()
    {
       

        Vector2 playerPos = GameManager.Instance.player.transform.position;
        Vector2 spawnPos;
        //���� ��ǥ���ϳ� ��´�.
        Vector2 ramPos = Random.insideUnitCircle;
        //���� ������ǥ ������ ���̰� 1�� ���͸� ���Ѵ�.
        Vector2 normalized = ramPos.normalized;
        //�߽� �������� ranPos�� Ȯ���� ������ ���Ѵ�
        float moveRad = minMaxDist.y - minMaxDist.x;// 5-3 =2
                                                    //������ŭ ������ ��ǥ�� ���Ѵ�.       
        Vector2 movedPos = ramPos * moveRad;
        // minDist �̳����� ��ǥ�� ������ �ȵǹǷ�, ���� ��ǥ�� �ش� �������� minDist ��ŭ ������ ���͸� ���Ѵ�.
        Vector2 notSpawnAreaVector = normalized *minMaxDist.x;
        //������ ��ǥ�� minDist ��ŭ ������ ���͸� ���Ѵ�.
        spawnPos = movedPos + notSpawnAreaVector;

        //spawnPos = (ramPos *(minMaxDist.y - minMaxDist.x)) + (ramPos.normalized * minMaxDist.x);

        return playerPos + spawnPos;
    
        
    }
}
