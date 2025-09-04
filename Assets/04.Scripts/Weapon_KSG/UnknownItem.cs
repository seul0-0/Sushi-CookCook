using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownItem : MonoBehaviour
{
    public void BuyRicePaddle()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[1].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[1].price);
        // 이 버튼이 속한 부모(UnknownItem)를 찾아서 비활성화
        gameObject.SetActive(false);
    }
    public void BuyKnife()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[2].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[2].price);
        gameObject.SetActive(false);
    }

    public void BuyChoppingBoard()
    {
        if (StatusManager.Instance.currentStatus.money < EquipManager.Instance.originalWeaponDatas[3].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-EquipManager.Instance.originalWeaponDatas[3].price);
        gameObject.SetActive(false);
    }

}
