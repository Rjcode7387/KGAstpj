using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Enpool;
    public Enemy enemyprefab;


    private void Awake()
    {
        Enpool = this;
    }

    List<Enemy> poolList = new();

    public Enemy Pop()
    {
        if (poolList.Count <= 0) //²¨³¾ °´Ã¼ ¾øÀ½
            Push(Instantiate(enemyprefab));

        Enemy enemy = poolList[0];

        poolList.Remove(enemy);

        enemy.gameObject.SetActive(true);
        enemy.transform.SetParent(null);

        return enemy;
    }
    public void Push(Enemy enemy)
    {
        poolList.Add(enemy);
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(transform, false);
    }
    public void Push(Enemy enemy, float delay)
    {
        StartCoroutine(PushCoroutine(enemy, delay));
    }
    IEnumerator PushCoroutine(Enemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(enemy);
    }
}
