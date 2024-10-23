using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��ä ������ �Ѱ��ϴ� ������Ʈ
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    
    internal List<Enemy> enemies = new List<Enemy>();//�����ϴ� ���� ����Ʈ
    internal Player player;//���� �����ϴ� Player �����ϴ� �÷��̾�� �ϳ��̱⶧��

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
            Destroy(enemy.gameObject); // �� ������Ʈ ����
        }
        enemies.Clear(); // ����Ʈ �ʱ�ȭ
    }
    public void HealPlayer(float amount)
    {
        if (player != null)
        {
            player.Heal(amount);
        }
    }


}
