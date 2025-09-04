using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator
{
    private PlayerDataM _playerData;  // IPlayerData ��� ��ü Ŭ���� ���
    private List<WeaponDataSO> _weaponDB;

    public EventMediator(PlayerDataM playerData, List<WeaponDataSO> weaponDB)
    {
        _playerData = playerData;
        _weaponDB = weaponDB;
    }

    // --- ��� ���� ---
    public void AddGold(int amount) => _playerData.AddGold(amount);
    public void SpendGold(int amount) => _playerData.SpendGold(amount);
    public int GetGold() => _playerData.Gold;


    // --- ���� ���� ---
    public void EquipWeapon(int slotIndex, string weaponName, int enhanceLevel)
    {
        var weapon = _weaponDB.Find(w => w.name == weaponName);
        if (weapon != null)
            _playerData.EquipWeapon(slotIndex, weapon, enhanceLevel);
    }

    public string[] GetEquippedWeaponNames() => _playerData.GetEquippedWeaponNames();
    public int[] GetWeaponEnhanceLevels() => _playerData.GetWeaponEnhanceLevels();

    // --- ���׷��̵� ---
    public void SetUpgradeLevel(UpgradeType type, int level)
    {
        _playerData.SetUpgradeLevel(type, level);
    }

    public IReadOnlyDictionary<UpgradeType, int> GetUpgradeLevels() => _playerData.UpgradeLevels;

    // --- ����/�ε� ---
    public void SaveGame() => SaveManager.SaveGame(_playerData);
    public void LoadGame()
    {
        SaveManager.LoadGame(_playerData, _weaponDB);
    }

    // --- ���� ��� ---
    public Stats GetEffectiveStats() => _playerData.GetEffectiveStats();
}
