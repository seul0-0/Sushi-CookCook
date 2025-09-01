using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Item/Weapon")]
public class WeaponDataTest : ScriptableObject
{
    public string weaponName;
    public Stats baseStats;

    // ��ȭ ������ ���� ���
    public Stats GetStats(int enhanceLevel)
    {
        return new Stats
        {
            Attack = baseStats.Attack + enhanceLevel * 2,
            CriticalChance = baseStats.CriticalChance + enhanceLevel * 0.01f,
            CriticalDamage = baseStats.CriticalDamage + enhanceLevel * 0.05f,
            AbilityAttack = baseStats.AbilityAttack + enhanceLevel,
            GoldBonus = baseStats.GoldBonus
        };
    }
}