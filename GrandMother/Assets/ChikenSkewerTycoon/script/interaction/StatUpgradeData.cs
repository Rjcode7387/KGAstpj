using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradeData
{
    public int Level { get; private set; } = 1;
    public int[] UpgradeCosts;

    public StatUpgradeData(int initialLevel, params int[] costs)
    {
        Level = initialLevel;
        UpgradeCosts = costs;
    }

    public int GetNextUpgradeCost()
    {
        return Level > UpgradeCosts.Length ? int.MaxValue : UpgradeCosts[Level - 1];
    }


    public bool CanUpgrade(float playerMoney)
    {
        return Level <= UpgradeCosts.Length && playerMoney >= GetNextUpgradeCost();
    }

    public bool Upgrade()
    {
        if (Level > UpgradeCosts.Length) return false;
        Level++;
        return true;
    }
}
