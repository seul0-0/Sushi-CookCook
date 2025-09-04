using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotButton : MonoBehaviour
{
    public int index;

    public Button upgradeBtn;
    public Button equipBtn;

    public EquipmentUI equipUI;

    private void Start()
    {
        upgradeBtn.onClick.AddListener(CheckMoneyToEnhance);
        equipBtn.onClick.AddListener(WeaponEquip);
    }

    private void CheckMoneyToEnhance()
    {
        if (StatusManager.Instance.currentStatus.money < 10)
            return;
        StatusManager.Instance.ChangeMoneyValue(-10);

        UpgradeEquip();
    }

    public void UpgradeEquip()
    {
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

    }

    public void WeaponEquip()
    {
        EquipManager.Instance.currentWeapon.Clear();
        EquipManager.Instance.currentWeapon.Add(equipUI.weaponDatas[index]);

        equipUI.UpdateUI();

        equipUI.SetCurrentWeapon(equipUI.weaponDatas[index]);
    }

}
