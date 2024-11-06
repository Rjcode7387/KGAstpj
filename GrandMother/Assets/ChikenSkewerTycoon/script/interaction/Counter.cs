using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //ī���Ϳ��� �Ұ�

    //�߲�ġ�� �ڵ��Ǹ� �Ͽ� �Ǹ� �ݾ��� �÷��̾� �����ݿ� +��Ų��. 

    public float basePrice = 4f;//�⺻�߲�ġ �Ǹűݾ�
    private float priceMultiplier = 1f;

    public void SellChickenSkewers(Player player)
    {
        if (player == null || player.holdingChickenSkewers.Count <= 0) return;

        float totalAmount = CalculateSaleAmount(player.holdingChickenSkewers.Count);
        GameManager.Instance.AddMoney(totalAmount);
        ClearPlayerSkewers(player);
    }

    public void UpgradePriceMultiplier()
    {
        priceMultiplier += 0.5f;
    }

    public void IncreaseBasePrice(float amount)
    {
        basePrice += amount;
    }

    private float CalculateSaleAmount(int skewerCount)
    {
        return skewerCount * basePrice * priceMultiplier;
    }

    private void ClearPlayerSkewers(Player player)
    {
        foreach (GameObject skewer in player.holdingChickenSkewers)
        {
            if (skewer != null)
            {
                Destroy(skewer);
            }
        }
        player.holdingChickenSkewers.Clear();
    }
}
