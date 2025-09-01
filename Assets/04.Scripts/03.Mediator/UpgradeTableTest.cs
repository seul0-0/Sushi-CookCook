using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Attack,
    CriticalChance,
    CriticalDamage,
    AbilityAttack,
    GoldBonus
}

public static class UpgradeTableTest
{
    // 레벨별 스탯 증가
    public static Stats GetStats(UpgradeType type, int level)
    {
        Stats stats = new Stats();

        switch (type)
        {
            case UpgradeType.Attack:
                stats.Attack = level * 5;
                break;
            case UpgradeType.CriticalChance:
                stats.CriticalChance = level * 0.01f;
                break;
            case UpgradeType.CriticalDamage:
                stats.CriticalDamage = 0.05f * level;
                break;
            case UpgradeType.AbilityAttack:
                stats.AbilityAttack = 1 * level;
                break;
            case UpgradeType.GoldBonus:
                stats.GoldBonus = 1 * level;
                break;
        }

        return stats;
    }
}