using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //ī���Ϳ��� �Ұ�

    //�߲�ġ�� �ڵ��Ǹ� �Ͽ� �Ǹ� �ݾ��� �÷��̾� �����ݿ� +��Ų��. 

    public float chickenSkewerPrice = 4f;//�⺻�߲�ġ �Ǹűݾ�
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

            // �÷��̾�� �� ����
            player.dollar += totalSaleAmount;

            // ��� �߲�ġ ������Ʈ �ѹ��� ����
            foreach (GameObject skewer in player.holdingChickenSkewers)
            {
                Destroy(skewer);
            }
            player.holdingChickenSkewers.Clear();



            print($"�Ǹ��� �߲�ġ: {player.holdingChickenSkewers.Count}��");
            print($"���� ������: {player.dollar}��");

          
        }
    }
    public void IncreaseSellingPrice(int amount)
    {
        chickenSkewerPrice += amount;
    }
}
