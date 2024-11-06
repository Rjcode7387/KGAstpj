using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //ī���Ϳ��� �Ұ�

    //�߲�ġ�� �ڵ��Ǹ� �Ͽ� �Ǹ� �ݾ��� �÷��̾� �����ݿ� +��Ų��. 

    public float chickenSkewerPrice = 4f;//�⺻�߲�ġ �Ǹűݾ�
    private float upgradePrice = 0.5f;

    public void UpgradeSellPrice()
    {
        upgradePrice += 0.5f;

    }
    public void sellandreceivemoney(Player player)
    {
        if (player.holdingChickenSkewers.Count > 0)
        {
            int totalSkewers = player.holdingChickenSkewers.Count;
            float totalSaleAmount = totalSkewers * chickenSkewerPrice * upgradePrice;

            GameManager.Instance.AddMoney(totalSaleAmount);
            player.dollar += totalSaleAmount;

            foreach (GameObject skewer in player.holdingChickenSkewers)
            {
                Destroy(skewer);
            }
            player.holdingChickenSkewers.Clear();
        }
    }
    public void IncreaseSellingPrice(int amount)
    {
        chickenSkewerPrice += amount;
    }
}
