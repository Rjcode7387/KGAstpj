using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("한번에 스폰될 적의 수.\nX:최소,Y:최대")]
    public Vector2Int minMaxCount;
    [Tooltip("스폰될 때 플레이어로부터의 최대/최소 거리\n x :최소, y :최대")]
    public Vector2Int minMaxDist;

    public GameObject enemyPrefab;
    public float spawnInterval; //생성간격

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
            Spawn(enemyCount);    // 몬스터 스폰
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
        //랜덤 좌표를하나 찍는다.
        Vector2 ramPos = Random.insideUnitCircle;
        //찍힌 랜덤좌표 방향의 길이가 1인 백터를 구한다.
        Vector2 normalized = ramPos.normalized;
        //중심 기준으로 ranPos가 확장할 범위를 구한다
        float moveRad = minMaxDist.y - minMaxDist.x;// 5-3 =2
                                                    //범위만큼 움직인 좌표를 구한다.       
        Vector2 movedPos = ramPos * moveRad;
        // minDist 이내에는 좌표가 찍히면 안되므로, 찍힌 좌표를 해당 방향으로 minDist 만큼 움직일 백터를 구한다.
        Vector2 notSpawnAreaVector = normalized *minMaxDist.x;
        //움직인 좌표에 minDist 만큼 움직인 백터를 더한다.
        spawnPos = movedPos + notSpawnAreaVector;

        //spawnPos = (ramPos *(minMaxDist.y - minMaxDist.x)) + (ramPos.normalized * minMaxDist.x);

        return playerPos + spawnPos;
    
        
    }
}
