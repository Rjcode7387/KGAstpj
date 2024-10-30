using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //카운터에서 할것

    //닭꼬치를 자동판매 하여 판매 금액을 플레이어 소지금에 +시킨다. 

    public float chickenSkewerPrice;//닭꼬치 판매금액

    public void sellandreceivemoney(Player player)
    {
        if (player.holdingChickenSkewers > 0)
        {
            float totalSaleAmount = player.holdingChickenSkewers * chickenSkewerPrice;
            player.dollar += totalSaleAmount;

            print($"닭꼬치{player.holdingChickenSkewers}");
            print($"현재 소지금 : {player.dollar}");

            player.holdingChickenSkewers = 0;
        }
    }
}
