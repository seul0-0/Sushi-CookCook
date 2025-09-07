using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public int Attack;
    public float CriticalChance;
    public float CriticalDamage;
    public int AbilityAttack;
    public int GoldBonus;

    // 기본 생성자
    public Stats() { }

    // 복사 생성자
    public Stats(Stats other)
    {
        Attack = other.Attack;
        CriticalChance = other.CriticalChance;
        CriticalDamage = other.CriticalDamage;
        AbilityAttack = other.AbilityAttack;
        GoldBonus = other.GoldBonus;
    }

    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats
        {
            Attack = a.Attack + b.Attack,
            CriticalChance = a.CriticalChance + b.CriticalChance,
            CriticalDamage = a.CriticalDamage + b.CriticalDamage,
            AbilityAttack = a.AbilityAttack + b.AbilityAttack,
            GoldBonus = a.GoldBonus + b.GoldBonus,
        };
    }
}


[CreateAssetMenu(fileName = "NewPlayerStat", menuName = "Player/StatsData")]
public class PlayerStatsTest : ScriptableObject
{
    public List<UpgradeTableSO> upgradeTables;

    [Header("Base Stats")]
    public Stats baseStats = new Stats
    {
        Attack = 1,
        CriticalChance = 0.0f,
        CriticalDamage = 1.5f,
        AbilityAttack = 1,
        GoldBonus = 0,
    };
}