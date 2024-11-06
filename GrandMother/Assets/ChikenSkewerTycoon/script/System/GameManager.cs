using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal Grill grill;
    public Player player;
    public UIManager uiManager;
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
        uiManager = FindAnyObjectByType<UIManager>();
        counter = FindAnyObjectByType<Counter>();

        if (grill == null || player == null || uiManager == null || counter == null)
        {
            Debug.LogError("Essential components are missing!");
        }
    }
    public void AddMoney(float amount)
    {
        player.dollar += amount;
        uiManager.UpdateMoneyText(player.dollar);
    }

    public bool SpendMoney(float amount)
    {
        if (player.dollar >= amount)
        {
            player.dollar -= amount;
            uiManager.UpdateMoneyText(player.dollar);
            return true;
        }
        return false;
    }

    // ´ß²¿Ä¡ °ü·Ã ¸Þ¼­µå
    public bool CanHoldMoreChickenSkewers()
    {
        return player.holdingChickenSkewers.Count < player.maxHoldingChickenSkewers;
    }

    public void SellChickenSkewers()
    {
        if (player != null && counter != null)
        {
            counter.sellandreceivemoney(player);
        }
    }
}
