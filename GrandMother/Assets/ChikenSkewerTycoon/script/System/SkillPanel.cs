using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    private Button[] upgradeButtons;
    private Text[] costTexts;
    private Text[] levelTexts;

    private StatUpgradeData moveSpeedData = new StatUpgradeData();
    private StatUpgradeData holdingCapacityData = new StatUpgradeData();
    private StatUpgradeData sellingPriceData = new StatUpgradeData();

    private Player player;
    private Counter counter;

}
