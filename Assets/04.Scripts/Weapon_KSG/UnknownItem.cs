using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownItem : MonoBehaviour
{
    private void Start()
    {
        for(int i =0; i < EquipManager.Instance.originalWeaponDatas.Count; i++)
        {
            if (EquipManager.Instance.originalWeaponDatas[i].have == true)
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void BuyRicePaddle()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[1].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[1].price);
        // 이 버튼이 속한 부모(UnknownItem)를 찾아서 비활성화

        EquipManager.Instance.weaponDatas[1].have = true;

        gameObject.SetActive(false);
    }
    public void BuyKnife()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[2].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[2].price);

        EquipManager.Instance.weaponDatas[2].have = true;

        gameObject.SetActive(false);
    }

    public void BuyChoppingBoard()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[3].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[3].price);

        EquipManager.Instance.weaponDatas[2].have = true;

        gameObject.SetActive(false);
    }

}
