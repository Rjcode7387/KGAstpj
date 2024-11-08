using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal Grill grill;
    public Player player;  
    public Counter counter;
    public float totalMoney => player.dollar;
    public int totalChickenSkewers => player.holdingChickenSkewers.Count;

    private void Awake()
    {
        InitializeSingleton();
        FindGameComponents();
    }
    private void InitializeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
        
    }
    private void FindGameComponents()
    {
        grill = FindAnyObjectByType<Grill>();
        player = FindAnyObjectByType<Player>();
        counter = FindAnyObjectByType<Counter>();
    }


    public void AddMoney(float amount)
    {
        if (amount <= 0 || player == null) return;

        player.dollar += amount;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateMoneyText(player.dollar);
        }
    }

    public bool SpendMoney(float amount)
    {
        if (amount <= 0 || player == null || player.dollar < amount) return false;

        player.dollar -= amount;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateMoneyText(player.dollar);
        }
        return true;
    }

    public bool CanHoldMoreChickenSkewers()
    {
        return player.holdingChickenSkewers.Count < player.maxHoldingChickenSkewers;
    }

    public void SellChickenSkewers()
    {
        if (player == null || counter == null) return;
        counter.SellChickenSkewers(player);
    }
}
