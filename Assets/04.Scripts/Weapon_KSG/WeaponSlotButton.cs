using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotButton : MonoBehaviour
{
    // === 버튼의 고유 번호 ===
    public int index;

    public Button upgradeBtn;
    public Button equipBtn;

    public EquipmentUI equipUI;

    private void Start()
    {
        upgradeBtn.onClick.AddListener(CheckMoneyToEnhance);
        equipBtn.onClick.AddListener(WeaponEquip);
    }

    // === 업그레이드 비용 확인 후 업그레이드 ===
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

    // === 장비 장착 ===
    public void WeaponEquip()
    {
        EquipManager.Instance.currentWeapon.Clear();
        EquipManager.Instance.currentWeapon.Add(equipUI.weaponDatas[index]);

        equipUI.UpdateUI();

        equipUI.SetCurrentWeapon(equipUI.weaponDatas[index]);
    }

}
