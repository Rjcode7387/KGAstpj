using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyprefabs;
    public float Interval = 1f;

    private void Start() 
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {          
            yield return new WaitForSeconds(Interval);

            Spawn(); 
        }

    }
    private void Spawn()
    {
        Instantiate(enemyprefabs);
    }
}
