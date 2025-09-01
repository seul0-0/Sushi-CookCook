using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("status")]                     // === �÷��̾� ���� ===
    public int attack = 1;
    public float critical = 0;
    public float criticalDamage = 1.5f; 
    public int luck = 0;                   // === ��� ��ġ ==

    [Header("recipt")]                    
    public int money = 10;

    // === ���׷��̵� ��ġ ===
    private int _attack_Upgrade = 1;
    private float _critical_Upgrade = 0.5f;
    private float _critical_Damage_Upgrade = 0.01f;
    private int _luck_Upgrade = 1;            

    public int UpgradeAttackValue(int bonus)
    {
        attack += (_attack_Upgrade * bonus);

        return attack ;
    }

    public float UpgradeCriticalValue(int bonus) 
    {
        float value = Mathf.Min(critical + (_critical_Upgrade * bonus), 100);

        critical = value;

        return critical;
    }

    public float UpgradeCriticalDamageValue(int bonus) 
    {
        criticalDamage += (_critical_Damage_Upgrade * bonus);

        return criticalDamage;
    }

    public int UpgradeLuckValue(int bonus)
    {
        luck += (_luck_Upgrade * bonus);

        return luck;
    }
}
