using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //ī���Ϳ��� �Ұ�

    //�߲�ġ�� �ڵ��Ǹ� �Ͽ� �Ǹ� �ݾ��� �÷��̾� �����ݿ� +��Ų��. 

    public float chickenSkewerPrice;//�߲�ġ �Ǹűݾ�

    public void sellandreceivemoney(Player player)
    {
        if (player.holdingChickenSkewers > 0)
        {
            float totalSaleAmount = player.holdingChickenSkewers * chickenSkewerPrice;
            player.dollar += totalSaleAmount;

            print($"�߲�ġ{player.holdingChickenSkewers}");
            print($"���� ������ : {player.dollar}");

            player.holdingChickenSkewers = 0;
        }
    }
}
