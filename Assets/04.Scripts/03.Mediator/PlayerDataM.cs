using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public interface IPlayerData
{
    void AddGold(int amount);
    void SpendGold(int amount);
}

public class PlayerDataM : IPlayerData
{
    // ����
    private WeaponDataSO[] _weaponSlots = new WeaponDataSO[5];
    private int[] _weaponEnhanceLevels = new int[5];

    // ���׷��̵� ����
    private Dictionary<UpgradeType, int> _upgradeLevels;
    public IReadOnlyDictionary<UpgradeType, int> UpgradeLevels => _upgradeLevels;

    // Upgrade ���̺� ����
    private Dictionary<UpgradeType, UpgradeTableSO> _upgradeTables;
    public int Gold { get; private set; }

    public PlayerDataM(List<UpgradeTableSO> upgradeTables)
    {
        _upgradeLevels = new Dictionary<UpgradeType, int>();
        _upgradeTables = new Dictionary<UpgradeType, UpgradeTableSO>();

        foreach (var table in upgradeTables)
        {
            _upgradeTables[table.type] = table;
            _upgradeLevels[table.type] = 0; // �ʱ� ����
        }
    }

    // PlayerStatsTest�� ������
    public PlayerDataM(PlayerStatsTest stats)
        : this(stats.upgradeTables) { }

    // --- ��� ���� ---
    public void EquipWeapon(int slotIndex, WeaponDataSO weapon, int enhanceLevel)
    {
        if (slotIndex < 0 || slotIndex >= 5) return;

        _weaponSlots[slotIndex] = weapon;
        _weaponEnhanceLevels[slotIndex] = enhanceLevel;
    }
    // --- ���׷��̵� ---
    public void SetUpgradeLevel(UpgradeType type, int level)
    {
        _upgradeLevels[type] = level;
    }

    // --- ��� ---
    public void AddGold(int amount) => Gold += amount;
    public void SpendGold(int amount) => Gold -= amount;


    public Stats GetEffectiveStats()
    {
        Stats total = new Stats();

        // ���⿡�� ���� �ջ�
        for (int i = 0; i < _weaponSlots.Length; i++)
        {
            var weapon = _weaponSlots[i];
            if (weapon != null)
            {
                total += weapon.GetStatsByEnhanceLevel(_weaponEnhanceLevels[i]);
            }
        }

        // ���׷��̵� ���ʽ� �ջ�
        foreach (var kvp in _upgradeLevels)
        {
            UpgradeType type = kvp.Key;
            int level = kvp.Value;

            if (_upgradeTables.TryGetValue(type, out UpgradeTableSO table))
            {
                total += table.GetBonus(level);
            }
        }
        return total;
    }



    // ����ȭ ��ȯ�޼���
    public string[] GetEquippedWeaponNames()
    {
        return _weaponSlots.Select(w => w != null ? w.name : null).ToArray();
    }

    public int[] GetWeaponEnhanceLevels()
    {
        return _weaponEnhanceLevels.ToArray();
    }

    public void LoadFromSave(PlayerSaveData data, List<WeaponDataSO> weaponDB)
    {
        // ���� ���� ����
        for (int i = 0; i < data.equippedWeaponNames.Length; i++)
        {
            var weaponName = data.equippedWeaponNames[i];
            if (!string.IsNullOrEmpty(weaponName))
            {
                var weapon = weaponDB.FirstOrDefault(w => w.name == weaponName);
                _weaponSlots[i] = weapon;
            }
            _weaponEnhanceLevels[i] = data.weaponEnhanceLevels[i];
        }

        // ���׷��̵� ����
        foreach (var kvp in data.upgradeLevels)
        {
            _upgradeLevels[kvp.Key] = kvp.Value;
        }

        // ��� ����
        Gold = data.gold;
    }
}
