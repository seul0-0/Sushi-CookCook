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
    public int criticalLevel = 0;
    public int criticalDamageLevel = 0;
    public int luckLevel = 0;            

    // === ������ ���� ��·� ===
    public int UpgradeAttackValue()
    {
        attackLevel++;

        attack += 1;

        return attack ;
    }

    // === ���� ���� ������ ǥ�� ===
    public int CalculateNextAttackValue()
    {
        return attack + 1;
    }

    // === ������ ũ��Ƽ�� ��·� ===
    public float UpgradeCriticalValue() 
    {
        criticalLevel++;

        float value = Mathf.Min(critical + 0.5f, 100);

        critical = value;

        return critical;
    }

    // === ���� ũ��Ƽ�� ������ ǥ�� ===
    public float CalculateNextCriticalValue()
    {
        return critical + 0.5f;
    }

    // === ������ ũ��Ƽ�� ������ ǥ�� ===
    public float UpgradeCriticalDamageValue() 
    {
        criticalDamageLevel++;

        criticalDamage +=  0.01f;

        return criticalDamage;
    }
    
    // === ���� ũ��Ƽ�� ������ ������ ǥ�� ===
    public float CalculateNextCriticalDamageValue()
    {
        return criticalDamage + 0.01f;
    }

    // === ������ �� ǥ�� ===
    public int UpgradeLuckValue()
    { 
        luckLevel++;

        luck += 1;

        return luck;
    }

    // === ���� �� ������ ǥ�� ==
    public int CalculateNextLuckValue()
    {
        return luck + 1;
    }

    // === �Է��� ��ŭ ���� + �� �ּ� 0 ===
    public int ChangeMoney(int amount)
    {
        money = Mathf.Max(0, money + amount);

        return money;
    }
}
