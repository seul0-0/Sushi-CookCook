using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator
{
    public IPlayerData PlayerData { get; private set; }  // �������̽��� ����

    public EventMediator(IPlayerData playerData)
    {
        PlayerData = playerData;
    }

    // ��� ���� ��û
    public void EquipWeapon(WeaponDataTest weapon, int enhanceLevel)
    {
        PlayerData.EquipWeapon(weapon, enhanceLevel);
        OnStatsChanged();
    }

    // ���׷��̵� ��û
    public void UpgradeStat(UpgradeType type, int level)
    {
        PlayerData.SetUpgradeLevel(type, level);
        OnStatsChanged();
    }

    // ��� ���� ��û
    public void AddGold(int amount) => PlayerData.AddGold(amount);
    public void SpendGold(int amount) => PlayerData.SpendGold(amount);

    // ���� ���� �� �̺�Ʈ
    public event Action<Stats> OnStatsChangedEvent;

    private void OnStatsChanged()
    {
        OnStatsChangedEvent?.Invoke(PlayerData.EffectiveStats);
    }
}
