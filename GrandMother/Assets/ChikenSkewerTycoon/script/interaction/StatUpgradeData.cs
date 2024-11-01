using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradeData
{
    public int Level { get; private set; } = 1;
    public int[] UpgradeCosts = new int[] { 200, 500 }; // 각 레벨 업그레이드 비용

    public int GetNextUpgradeCost()
    {
        return Level >= 3 ? 0 : UpgradeCosts[Level - 1];
    }

    public bool CanUpgrade(float playerMoney)
    {
        return Level < 3 && playerMoney >= GetNextUpgradeCost();
    }

    public bool Upgrade()
    {
        if (Level >= 3) return false;
        Level++;
        return true;
    }
}
