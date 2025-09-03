using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    // === PlayerStatus 스크립터블 오브젝트를 받음 ===
    public PlayerStatus status;

    public static Action OnMoneyChanged;   // === 돈 변화 델리게이트 ===

    public PlayerStatus currentStatus;

    private void Start()
    {
        currentStatus = Instantiate(status);
    }

    // === Player Stat 찾기 ===
    public int GetStatType(StatType type)
    {
        for (int i = 0; i < currentStatus.stats.Length; i++)
        {
            if (currentStatus.stats[i].type == type)
            {
                return i;
            }
        }
        return 0;
    }

    // === 다음 강화 수치 ===
    public float NextStatValueDisplay(StatType type)
    {
        int index = GetStatType(type);

        switch (type)
        {
            case StatType.attack:
                return currentStatus.stats[index].value + 1;

            case StatType.critical:
                float value = Mathf.Min(currentStatus.stats[index].value + 0.5f, 100);
                return value;

            case StatType.criticalDamage:
                return currentStatus.stats[index].value + 0.01f;

            case StatType.luck:
                return currentStatus.stats[index].value + 1;

            case StatType.autoattack:
                return currentStatus.stats[index].value + 2;

            default:
                return 0f;

        }
    }

    // === 스텟 업그레이드 ===
    public float UpgradeValue(StatType type)
    {
        int index = GetStatType(type);

        switch (type)
        {
            case StatType.attack:
                currentStatus.stats[index].level++;

                currentStatus.stats[index].value += 1;

                return currentStatus.stats[index].value;

            case StatType.critical:
                currentStatus.stats[index].level++;

                float value = Mathf.Min(currentStatus.stats[index].value + 0.5f, 100);

                currentStatus.stats[index].value = value;

                return value;

            case StatType.criticalDamage:
                currentStatus.stats[index].level++;

                currentStatus.stats[index].value += 1 / 100f;

                return currentStatus.stats[index].value;

            case StatType.luck:
                currentStatus.stats[index].level++;

                currentStatus.stats[index].value += 1;

                return currentStatus.stats[index].value;

            case StatType.autoattack:
                currentStatus.stats[index].level++;

                currentStatus.stats[index].value += 2;

                return currentStatus.stats[index].value;

            default:
                return 0f;

        }
    }

    // === 입력한 만큼 돈이 + 됨 최소 0 ===
    public int ChangeMoneyValue(int amount)
    {
        currentStatus.money = Mathf.Max(0, currentStatus.money + amount);

        OnMoneyChanged?.Invoke();

        return currentStatus.money;
    }

    // === 강화시 사용되는 돈 ===
    public int CheckMoney(StatType type)
    {
        int index = GetStatType(type);

        return type switch
        {
            StatType.attack => 1 * (currentStatus.stats[index].level + 1),
            StatType.critical => 2 * (currentStatus.stats[index].level + 1),
            StatType.criticalDamage => 5 * (currentStatus.stats[index].level + 1),
            StatType.luck => 10 * (currentStatus.stats[index].level + 1),
            StatType.autoattack => 100 * (currentStatus.stats[index].level + 1),
            _ => 0,
        };
    }
}
