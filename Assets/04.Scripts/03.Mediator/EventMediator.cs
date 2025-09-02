using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator
{
    public IPlayerData PlayerData { get; private set; }  // 인터페이스만 노출

    public EventMediator(IPlayerData playerData)
    {
        PlayerData = playerData;
    }

    // 장비 장착 요청
    public void EquipWeapon(WeaponDataTest weapon, int enhanceLevel)
    {
        PlayerData.EquipWeapon(weapon, enhanceLevel);
        OnStatsChanged();
    }

    // 업그레이드 요청
    public void UpgradeStat(UpgradeType type, int level)
    {
        PlayerData.SetUpgradeLevel(type, level);
        OnStatsChanged();
    }

    // 골드 증감 요청
    public void AddGold(int amount) => PlayerData.AddGold(amount);
    public void SpendGold(int amount) => PlayerData.SpendGold(amount);

    // 스탯 변경 시 이벤트
    public event Action<Stats> OnStatsChangedEvent;

    private void OnStatsChanged()
    {
        OnStatsChangedEvent?.Invoke(PlayerData.EffectiveStats);
    }
}
