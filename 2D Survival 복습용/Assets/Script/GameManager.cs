using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 전채 진행을 총괄하는 오브젝트
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    
    internal List<Enemy> enemies = new List<Enemy>();//존재하는 적에 리스트
    internal Player player;//씬에 존해하는 Player 존재하는 플레이어는 하나이기때문

    //interface List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //if (instance != null)
        //{
        //    DestroyImmediate(this);
        //    return;
        //}
        //instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        
    }
    public void RemoveAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject); // 적 오브젝트 삭제
        }
        enemies.Clear(); // 리스트 초기화
    }
    public void HealPlayer(float amount)
    {
        if (player != null)
        {
            player.Heal(amount);
        }
    }


}
