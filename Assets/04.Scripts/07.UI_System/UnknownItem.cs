using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownItem : MonoBehaviour
{
    [SerializeField]
    private int index;

    private void Start()
    {
        EquipmentUI.OnEquip += CheckUnknowItem;
    }

    // === �ر� ���� üũ ===
    public void CheckUnknowItem()
    {
        if (EquipManager.Instance.weaponDatas[index].have == true)
        {
            gameObject.SetActive(false);
        }
        else 
        {
            gameObject.SetActive(true);
        }
    }

    public void BuyWeapon()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[index].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[index].price);

        EquipManager.Instance.weaponDatas[index].have = true;

        gameObject.SetActive(false);
    }

}
