using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Button[] upgradeButtons;
    public Text[] costTexts;

    public Image[] skill1Stars;
    public Image[] skill2Stars;
    public Image[] skill3Stars;


    private StatUpgradeData moveSpeedData = new StatUpgradeData(1, 200, 500);
    private StatUpgradeData holdingCapacityData = new StatUpgradeData(1, 200, 500);
    private StatUpgradeData sellingPriceData = new StatUpgradeData(1, 200, 500);

    private Player player;
    private Counter counter;

    private void Start()
    {
        player = GameManager.Instance.player;
        counter = GameManager.Instance.counter; 
        InitializeUI();
        gameObject.SetActive(false);
    }

    private void InitializeUI()
    {
        upgradeButtons = GetComponentsInChildren<Button>();
        costTexts = GetComponentsInChildren<Text>();

        InitializeStars(skill1Stars);
        InitializeStars(skill2Stars);
        InitializeStars(skill3Stars);


        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            upgradeButtons[i].onClick.AddListener(() => UpgradeSkill(index));
        }
    }
    private void InitializeStars(Image[] stars)
    {
        if (stars != null && stars.Length > 0)
        {
            stars[0].color = Color.white;
            for (int i = 1; i < stars.Length; i++)
            {
                stars[i].color = new Color(1, 1, 1, 0.2f);
            }
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
                counter.UpgradePriceMultiplier();
                break;
        }
    }

    private void UpdateUI(int index, StatUpgradeData data)
    {
        int nextCost = data.GetNextUpgradeCost();
        if (nextCost == int.MaxValue)
        {
            costTexts[index].text = "MAX!";
        }
        else
        {
            costTexts[index].text = $"{nextCost}";
        }
        UpdateStars(index, data.Level); // 필요 시 별 이미지 업데이트
    }
    private void UpdateStars(int index, int level)
    {
        Image[] stars = index switch
        {
            0 => skill1Stars,
            1 => skill2Stars,
            2 => skill3Stars,
            _ => null
        };

        if (stars != null)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].color = i < level ? Color.white : new Color(1, 1, 1, 0.2f);
            }
        }
    }


}

    




