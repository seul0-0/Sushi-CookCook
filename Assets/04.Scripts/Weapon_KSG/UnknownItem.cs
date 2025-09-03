using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownItem : MonoBehaviour
{
    public void BuyRicePaddle()
    {
        if (StatusManager.Instance.currentStatus.money < 30)
            return;
        StatusManager.Instance.ChangeMoneyValue(-30);
        // �� ��ư�� ���� �θ�(UnknownItem)�� ã�Ƽ� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
    public void BuyChoppingBoard()
    {
        if (StatusManager.Instance.currentStatus.money < 100)
            return;
        StatusManager.Instance.ChangeMoneyValue(-100);
        gameObject.SetActive(false);
    }
    public void BuyKnife()
    {
        if (StatusManager.Instance.currentStatus.money < 100)
            return;
        StatusManager.Instance.ChangeMoneyValue(-100);
        gameObject.SetActive(false);
    }
}
