using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotButton : MonoBehaviour
{
    public static Action OnWeaponChanhged;            // === 델리 게이트 호출 ===

    // === 버튼의 고유 번호 ===
    public int index;

    public Button upgradeBtn;
    public Button equipBtn;

    public EquipmentUI equipUI;

    private bool _isUpgrade;

    private void Start()
    {
        upgradeBtn.onClick.AddListener(UpgradeEquip);
        equipBtn.onClick.AddListener(WeaponEquip);

        CheckMoneyToEnhance();

        // === 돈 변화 감지후 ui 갱신 ===
        StatusManager.OnMoneyChanged += CheckMoneyToEnhance;
    }

    // === 업그레이드 비용 확인 후 업그레이드 ===
    private void CheckMoneyToEnhance()
    {
        // === 돈이 부족할 경우 ===
        if (StatusManager.Instance.currentStatus.money < 10)
        {
            upgradeBtn.image.color = Color.red;
            return;
        }

        upgradeBtn.image.color = Color.black;

        _isUpgrade = true;
    }

    public void UpgradeEquip()
    {
        if (_isUpgrade == false) return;

        StatusManager.Instance.ChangeMoneyValue(-10);

        if (EquipManager.Instance.currentWeapon[0] == equipUI.weaponDatas[index])
        {
            StatusManager.Instance.currentStatus.stats[(int)StatType.attack].value -= EquipManager.Instance.currentWeapon[0].ItemAttack;
            StatusManager.Instance.currentStatus.stats[(int)StatType.critical].value -= EquipManager.Instance.currentWeapon[0].CriticalChance;
        }

        equipUI.weaponDatas[index].ItemLevel++;

        switch (index)
        {
            case 0:
                equipUI.weaponDatas[index].ItemAttack += 1;
                break;
            case 1:
                equipUI.weaponDatas[index].ItemAttack += 2;
                break;
            case 2:
                equipUI.weaponDatas[index].ItemAttack += 4;
                break;
            case 3:
                equipUI.weaponDatas[index].ItemAttack += 4;
                break;
        }

        equipUI.UpdateUI();

        if (EquipManager.Instance.currentWeapon[0] == equipUI.weaponDatas[index])
        {
            equipUI.SetCurrentWeapon(equipUI.weaponDatas[index]);
        }

        OnWeaponChanhged?.Invoke();
    }

    // === 장비 장착 ===
    public void WeaponEquip()
    {
        StatusManager.Instance.currentStatus.stats[(int)StatType.attack].value -= EquipManager.Instance.currentWeapon[0].ItemAttack;
        StatusManager.Instance.currentStatus.stats[(int)StatType.critical].value -= EquipManager.Instance.currentWeapon[0].CriticalChance;

        EquipManager.Instance.currentWeapon.Clear();
        EquipManager.Instance.currentWeapon.Add(equipUI.weaponDatas[index]);

        equipUI.UpdateUI();

        equipUI.SetCurrentWeapon(equipUI.weaponDatas[index]);
    }
}
