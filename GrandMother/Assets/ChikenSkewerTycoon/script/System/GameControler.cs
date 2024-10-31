using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public UnityEngine.UI.Text moneyText;
    public Player player;

    void Update()
    {
        moneyText.text =$"£Ü{player.dollar}";
    }
}
