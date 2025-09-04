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
        // �� ��ư�� ���� �θ�(UnknownItem)�� ã�Ƽ� ��Ȱ��ȭ
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
