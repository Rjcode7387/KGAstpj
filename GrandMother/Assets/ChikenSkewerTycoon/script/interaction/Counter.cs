using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //카운터에서 할것

    //닭꼬치를 자동판매 하여 판매 금액을 플레이어 소지금에 +시킨다. 

    public float chickenSkewerPrice = 50f;//기본닭꼬치 판매금액
    private float upgradePrice = 1f;

    public void UpgradeSellPrice()
    {
        upgradePrice += 0.2f;

    }
    public void sellandreceivemoney(Player player)
    {
        if (player.holdingChickenSkewers > 0)
        {
            float totalSaleAmount = player.holdingChickenSkewers * chickenSkewerPrice*upgradePrice;
            player.dollar += totalSaleAmount;

            print($"닭꼬치{player.holdingChickenSkewers}");
            print($"현재 소지금 : {player.dollar}");

            player.holdingChickenSkewers = 0;
        }
    }
}
