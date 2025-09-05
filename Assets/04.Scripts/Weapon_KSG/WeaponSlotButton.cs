using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotButton : MonoBehaviour
{
    public static Action OnWeaponChanhged;            // === ���� ����Ʈ ȣ�� ===

    // === ��ư�� ���� ��ȣ ===
    public int index;

    public Button upgradeBtn;
    public Button equipBtn;

    public EquipmentUI equipUI;

    private bool _isUpgrade;

    private EquipManager _equipManager;

    private void Start()
    {
        upgradeBtn.onClick.AddListener(UpgradeEquip);
        equipBtn.onClick.AddListener(WeaponEquip);

        CheckMoneyToEnhance();

        _equipManager = EquipManager.Instance;

        // === �� ��ȭ ������ ui ���� ===
        StatusManager.OnMoneyChanged += CheckMoneyToEnhance;
    }

    // === ���׷��̵� ��� Ȯ�� �� ���׷��̵� ===
    private void CheckMoneyToEnhance()
    {
        // === ���� ������ ��� ===
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

        if (EquipManager.Instance.currentWeapon[0] == _equipManager.weaponDatas[index])
        {
            StatusManager.Instance.currentStatus.stats[(int)StatType.attack].value -= _equipManager.currentWeapon[0].ItemAttack;
            StatusManager.Instance.currentStatus.stats[(int)StatType.critical].value -= _equipManager.currentWeapon[0].CriticalChance;
        }

        _equipManager.weaponDatas[index].ItemLevel++;

        switch (index)
        {
            case 0:
                _equipManager.weaponDatas[index].ItemAttack += 1;
                break;
            case 1:
                _equipManager.weaponDatas[index].ItemAttack += 2;
                break;
            case 2:
                _equipManager.weaponDatas[index].ItemAttack += 4;
                break;
            case 3:
                _equipManager.weaponDatas[index].ItemAttack += 4;
                break;
        }

        equipUI.UpdateUI();

        if (EquipManager.Instance.currentWeapon[0] == _equipManager.weaponDatas[index])
        {
            equipUI.SetCurrentWeapon(_equipManager.weaponDatas[index]);
        }

        OnWeaponChanhged?.Invoke();
    }

    // === ��� ���� ===
    public void WeaponEquip()
    {
        StatusManager.Instance.currentStatus.stats[(int)StatType.attack].value -= _equipManager.currentWeapon[0].ItemAttack;
        StatusManager.Instance.currentStatus.stats[(int)StatType.critical].value -= _equipManager.currentWeapon[0].CriticalChance;

        _equipManager.EquipItem(_equipManager.weaponDatas[index], 0); // ���� 0�� ����

        StatusManager.Instance.currentStatus.stats[(int)StatType.attack].value += _equipManager.currentWeapon[0].ItemAttack;
        StatusManager.Instance.currentStatus.stats[(int)StatType.critical].value += _equipManager.currentWeapon[0].CriticalChance;

        equipUI.UpdateUI(); // UI ����

        OnWeaponChanhged?.Invoke();
    }
}
