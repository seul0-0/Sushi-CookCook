using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    attack,
    critical,
    criticalDamage,
    luck,
    autoattack
}

[Serializable]
public struct StatData
{
    public StatType type;
    public float value;
    public int level;
}

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    public static Action OnMoneyChanged;   // === �� ��ȭ ��������Ʈ ===

    [Header("Status")]                     // === �÷��̾� ���� ===
    public StatData[] stats;

    [Header("Recipt")]                    
    public int money = 10;

    // === Player Stat ã�� ===
    public int GetStatType(StatType type)
    {
        for (int i = 0; i < stats.Length; i++) 
        {
           if( stats[i].type == type)
           {
                return i;
           }
        }
        return 0;
    }

    // === ���� ��ȭ ��ġ ===
    public float NextStatValueDisplay(StatType type)
    {
        int index = GetStatType(type);

        switch (type)
        {
            case StatType.attack:
                return stats[index].value + 1;

            case StatType.critical:
                float value = Mathf.Min(stats[index].value + 0.5f, 100);
                return value;

            case StatType.criticalDamage:
                return stats[index].value + 0.01f;

            case StatType.luck:
                return stats[index].value + 1;

            case StatType.autoattack:
                return stats[index].value + 2;

            default:
                return 0f;

        }
    }

    // === ���� ���׷��̵� ===
    public float UpgradeValue(StatType type)
    {
        int index = GetStatType(type);

        switch (type)
        {
            case StatType.attack:
                stats[index].level++;

                stats[index].value += 1;

                return stats[index].value;

            case StatType.critical:
                stats[index].level++;

                float value = Mathf.Min(stats[index].value + 0.5f, 100);

                stats[index].value = value;

                return value;

            case StatType.criticalDamage:
                stats[index].level++;

                stats[index].value += 0.01f;

                return stats[index].value;

            case StatType.luck:
                stats[index].level++;

                stats[index].value += 1;

                return stats[index].value;

            case StatType.autoattack:
                stats[index].level++;

                stats[index].value += 2;

                return stats[index].value;

            default:
                return 0f;

        }
    }

    // === �Է��� ��ŭ ���� + �� �ּ� 0 ===
    public int ChangeMoneyValue(int amount)
    {
        money = Mathf.Max(0, money + amount);

        OnMoneyChanged?.Invoke();

        return money;
    }

    // === ��ȭ�� ���Ǵ� �� ===
    public int CheckMoney(StatType type)
    {
        int index = GetStatType(type);

        return type switch
        {
            StatType.attack => 1 * (stats[index].level + 1),
            StatType.critical => 2 * (stats[index].level + 1),
            StatType.criticalDamage => 5 * (stats[index].level + 1),
            StatType.luck => 10 * (stats[index].level + 1),
            StatType.autoattack => 100 * (stats[index].level + 1),
            _ => 0,
        };
    }

}
