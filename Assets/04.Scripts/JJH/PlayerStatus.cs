using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("Status")]                     // === 플레이어 스텟 ===
    public int attack = 1;
    public float critical = 0;
    public float criticalDamage = 1.5f; 
    public int luck = 0;                   // === 행운 수치 ==

    [Header("Recipt")]                    
    public int money = 10;

    [Header("Level")]
    // === 현재 업그레이드 수치 ===
    public int attackLevel = 0;
    public int criticalLevel = 0;
    public int criticalDamageLevel = 0;
    public int luckLevel = 0;            

    // === 레벨당 공격 상승량 ===
    public int UpgradeAttackValue()
    {
        attackLevel++;

        attack += 1;

        return attack ;
    }

    // === 다음 공격 증가량 표시 ===
    public int CalculateNextAttackValue()
    {
        return attack + 1;
    }

    // === 레벨당 크리티컬 상승량 ===
    public float UpgradeCriticalValue() 
    {
        criticalLevel++;

        float value = Mathf.Min(critical + 0.5f, 100);

        critical = value;

        return critical;
    }

    // === 다음 크리티컬 증가량 표시 ===
    public float CalculateNextCriticalValue()
    {
        return critical + 0.5f;
    }

    // === 레벨당 크리티컬 데미지 표시 ===
    public float UpgradeCriticalDamageValue() 
    {
        criticalDamageLevel++;

        criticalDamage +=  0.01f;

        return criticalDamage;
    }
    
    // === 다음 크리티컬 데미지 증가량 표시 ===
    public float CalculateNextCriticalDamageValue()
    {
        return criticalDamage + 0.01f;
    }

    // === 레벨당 운 표시 ===
    public int UpgradeLuckValue()
    { 
        luckLevel++;

        luck += 1;

        return luck;
    }

    // === 다음 운 증가량 표시 ==
    public int CalculateNextLuckValue()
    {
        return luck + 1;
    }

    // === 입력한 만큼 돈이 + 됨 최소 0 ===
    public int ChangeMoney(int amount)
    {
        money = Mathf.Max(0, money + amount);

        return money;
    }
}
