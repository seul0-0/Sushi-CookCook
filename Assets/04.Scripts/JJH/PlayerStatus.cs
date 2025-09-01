using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("Status")]                     // === �÷��̾� ���� ===
    public int attack = 1;
    public float critical = 0;
    public float criticalDamage = 1.5f; 
    public int luck = 0;                   // === ��� ��ġ ==

    [Header("Recipt")]                    
    public int money = 10;

    [Header("Level")]
    // === ���� ���׷��̵� ��ġ ===
    public int attackLevel = 0;
    public float criticalLevel = 0;
    public float criticalDamageLevel = 0;
    public int luckLevel = 0;            

    // === ������ ��·� ===
    public int UpgradeAttackValue()
    {
        attack += (attackLevel);

        return attack ;
    }

    public float UpgradeCriticalValue() 
    {
        float value = Mathf.Min(critical + (criticalLevel * 0.5f), 100);

        critical = value;

        return critical;
    }

    public float UpgradeCriticalDamageValue() 
    {
        criticalDamage += (criticalDamageLevel * 0.01f);

        return criticalDamage;
    }

    public int UpgradeLuckValue()
    {
        luck += (luckLevel);

        return luck;
    }
}
