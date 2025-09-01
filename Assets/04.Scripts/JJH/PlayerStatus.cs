using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("status")]                     // === 플레이어 스텟 ===
    public int attack = 1;
    public float critical = 0;
    public float criticalDamage = 1.5f; 

    [Header("recipt")]                     // === 결산 ===
    public int money = 100;
    public int luck = 0;                   // === 행운 수치 ==

    // === 업그레이드 수치 ===
    private int _attack_Upgrade = 1;
    private float _critical_Upgrade = 0.5f;
    private float _critical_Damage_Upgrade = 0.01f;
    private int _luck_Upgrade = 1;            

    public int UpgradeAttackValue(int bonus)
    {
        return attack + (_attack_Upgrade * bonus);
    }

    public float UpgradeCriticalValue(int bonus) 
    {
        float value = Mathf.Min(critical + (_critical_Upgrade * bonus), 100);

        return value;
    }

    public float UpgradeCriticalDamageValue(int bonus) 
    {
        return criticalDamage + (_critical_Damage_Upgrade * bonus);
    }

    public int UpgradeLuckValue(int bonus)
    {
        return luck + (_luck_Upgrade * bonus);
    }
}
