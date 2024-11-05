using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //카운터에서 할것

    //닭꼬치를 자동판매 하여 판매 금액을 플레이어 소지금에 +시킨다. 

    public float chickenSkewerPrice = 4f;//기본닭꼬치 판매금액
    private float upgradePrice = 1f;

    public void UpgradeSellPrice()
    {
        upgradePrice += 0.2f;

    }
    public void sellandreceivemoney(Player player)
    {
        if (player.holdingChickenSkewers.Count > 0)
        {
            int totalSkewers = player.holdingChickenSkewers.Count;
            float totalSaleAmount = totalSkewers * chickenSkewerPrice * upgradePrice;

            // 플레이어에게 돈 지급
            player.dollar += totalSaleAmount;

            // 모든 닭꼬치 오브젝트 한번에 삭제
            foreach (GameObject skewer in player.holdingChickenSkewers)
            {
                Destroy(skewer);
            }
            player.holdingChickenSkewers.Clear();



            print($"판매한 닭꼬치: {player.holdingChickenSkewers.Count}개");
            print($"현재 소지금: {player.dollar}원");

          
        }
    }
    public void IncreaseSellingPrice(int amount)
    {
        chickenSkewerPrice += amount;
    }
}
