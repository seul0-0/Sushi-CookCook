using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public interface IPlayerData
{
    void EquipWeapon(WeaponDataTest weapon, int enhanceLevel);
    void SetUpgradeLevel(UpgradeType type, int level);
    void AddGold(int amount);
    void SpendGold(int amount);
    Stats EffectiveStats { get; }
}

public class PlayerDataM : IPlayerData
{
    private readonly ReactiveProperty<int> _gold;
    public IReadOnlyReactiveProperty<int> Gold => _gold;

    private PlayerStatsTest _statsData;   // ScriptableObject ����

    // ���� ��� + ��ȭ ���� ����
    private WeaponDataTest _equippedWeapon;
    private int _weaponEnhanceLevel;
    // ���׷��̵� Ÿ�Ժ� ���� ����
    private Dictionary<UpgradeType, int> _upgradeLevels;

    public PlayerDataM(PlayerStatsTest statsData)
    {
        _statsData = statsData;
        _upgradeLevels = new Dictionary<UpgradeType, int>();
        _gold = new ReactiveProperty<int>(0);
    }

    // --- ��� ---
    public void EquipWeapon(WeaponDataTest weapon, int enhanceLevel)
    {
        _equippedWeapon = weapon;
        _weaponEnhanceLevel = enhanceLevel;
    }

    // --- ���׷��̵� ---
    public void SetUpgradeLevel(UpgradeType type, int level)
    {
        _upgradeLevels[type] = level;
    }

    // ���� ���� ���
    public Stats EffectiveStats
    {
        get
        {
            Stats total = new Stats();
            total += _statsData.baseStats;

            if (_equippedWeapon != null)
                total += _equippedWeapon.GetStats(_weaponEnhanceLevel);

            foreach (var kvp in _upgradeLevels)
                total += UpgradeTableTest.GetStats(kvp.Key, kvp.Value);

            return total;
        }
    }

    // --- ��� ---
    public void AddGold(int amount) => _gold.Value += amount;
    public void SpendGold(int amount) => _gold.Value -= amount;
}
