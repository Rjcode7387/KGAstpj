using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //ī���Ϳ��� �Ұ�

    //�߲�ġ�� �ڵ��Ǹ� �Ͽ� �Ǹ� �ݾ��� �÷��̾� �����ݿ� +��Ų��. 

    public float chickenSkewerPrice = 50f;//�⺻�߲�ġ �Ǹűݾ�
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

            print($"�߲�ġ{player.holdingChickenSkewers}");
            print($"���� ������ : {player.dollar}");

            player.holdingChickenSkewers = 0;
        }
    }
}
