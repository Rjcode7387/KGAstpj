using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    //카운터에서 할것

    //닭꼬치를 자동판매 하여 판매 금액을 플레이어 소지금에 +시킨다. 

    public float basePrice = 4f;//기본닭꼬치 판매금액
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
