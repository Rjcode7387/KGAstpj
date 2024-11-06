using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSkewerSpawner : MonoBehaviour
{
    [SerializeField] public GameObject chickenSkewerPrefab;
    private Vector2 spawnOffset = new Vector2(0, 0.3f);
    private List<GameObject> activeChickenSkewers = new List<GameObject>();

    public void Initialize(GameObject prefab)
    {
        chickenSkewerPrefab = prefab;
    }
    public GameObject SpawnChickenSkewer(Vector3 position)
    {
        Vector2 spawnPos = (Vector2)position + spawnOffset;
        GameObject newSkewer = Instantiate(chickenSkewerPrefab, spawnPos, Quaternion.identity);
        activeChickenSkewers.Add(newSkewer);
        return newSkewer;
    }

    public GameObject TakeLastSkewer()
    {
        if (activeChickenSkewers.Count > 0)
        {
            GameObject skewer = activeChickenSkewers[activeChickenSkewers.Count - 1];
            activeChickenSkewers.RemoveAt(activeChickenSkewers.Count - 1);
            return skewer;
        }
        return null;
    }

    public void ClearSkewers()
    {
        foreach (GameObject skewer in activeChickenSkewers)
        {
            Destroy(skewer);
        }
        activeChickenSkewers.Clear();
    }
}
