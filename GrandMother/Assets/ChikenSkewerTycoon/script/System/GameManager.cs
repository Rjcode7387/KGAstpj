using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal Grill grill;//�˾ƵѰ�...
    public Player player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            grill = FindAnyObjectByType<Grill>();
            player = FindAnyObjectByType<Player>();

            if (grill == null || player == null)
            {
                
            }
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
