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

    private void Start()
    {
        upgradeBtn.onClick.AddListener(CheckMoneyToEnhance);
        equipBtn.onClick.AddListener(WeaponEquip);
    }

    // === ���׷��̵� ��� Ȯ�� �� ���׷��̵� ===
    private void CheckMoneyToEnhance()
    {
        if (StatusManager.Instance.currentStatus.money < 10)
            return;
        StatusManager.Instance.ChangeMoneyValue(-10);

        UpgradeEquip();
    }

    public void UpgradeEquip()
    {
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

    // === ��� ���� ===
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
