using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabs; // 利 橇府崎
    public float spawnIntaval; //积己埃拜

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnIntaval);
            
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(enemyPrefabs);
    }
}
