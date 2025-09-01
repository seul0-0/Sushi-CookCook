using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("status")]                     // === 플레이어 스텟 ===
    public int attack;
    public int critical;

    [Header("recipt")]                     // === 결산 ===
    public int money;
    public int luck;
     
    [Header("upgrade")]                    // === 업그레이드 수치 ===
    public int attackUpgrade;
    public int criticalUpgrade;
    public int luckUpgrade;

    public int UpgradeAttackValue(int bonus)
    {
        return attack + (attackUpgrade * bonus);
    }

    public int UpgradeCriticalValue(int bonus) 
    {
        return critical + (criticalUpgrade * bonus);
    }

    public int UpgradeLuckValue(int bonus)
    {
        return luck + (luckUpgrade * bonus);
    }
}
