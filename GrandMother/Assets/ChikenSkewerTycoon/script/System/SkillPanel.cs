using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Button[] upgradeButtons;
    public Text[] costTexts;


    private StatUpgradeData moveSpeedData = new StatUpgradeData(1, 200, 500);
    private StatUpgradeData holdingCapacityData = new StatUpgradeData(2, 200, 500);
    private StatUpgradeData sellingPriceData = new StatUpgradeData(2, 200, 500);

    private Player player;
    private Counter counter;

    private void Start()
    {
        player = GameManager.Instance.player;
        counter = FindAnyObjectByType<Counter>();
        InitializeUI();
        gameObject.SetActive(false);
    }

    private void InitializeUI()
    {
        upgradeButtons = GetComponentsInChildren<Button>();
        costTexts = GetComponentsInChildren<Text>();


        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            upgradeButtons[i].onClick.AddListener(() => UpgradeSkill(index));
        }
    }

    private void UpgradeSkill(int index)
    {
        StatUpgradeData data = null;
        switch (index)
        {
            case 0:
                data = moveSpeedData;
                break;
            case 1:
                data = holdingCapacityData;
                break;
            case 2:
                data = sellingPriceData;
                break;
        }

        if (data != null && player.dollar >= data.GetNextUpgradeCost())
        {
            player.dollar -= data.GetNextUpgradeCost();
            data.Upgrade();

            ApplySkillEffect(index, data.Level);
            UpdateUI(index, data);
        }
    }

    private void ApplySkillEffect(int index, int level)
    {
        switch (index)
        {
            case 0:
                player.moveDistance += 1;
                break;
            case 1:
                player.maxHoldingChickenSkewers += 2;
                break;
            case 2:
                counter.IncreaseSellingPrice(2);
                break;
        }
    }

    private void UpdateUI(int index, StatUpgradeData data)
    {
        int nextCost = data.GetNextUpgradeCost();
        if (nextCost == int.MaxValue)
        {
            costTexts[index].text = "Cost: MAX";
        }
        else
        {
            costTexts[index].text = $"Cost: {nextCost}";
        }
        // UpdateStars(index, data.Level); // 필요 시 별 이미지 업데이트
    }
}

    




