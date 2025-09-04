using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    [Header("Ui")]
    public TextMeshProUGUI currentAtk;
    public TextMeshProUGUI currentMoney;

    // === 현재의 스텟 정보를 알려주기 ===
    private void Start()
    {
        Refresh();

        UpgradeButtonUi.OnStatusRefreshed += Refresh;

        WeaponSlotButton.OnWeaponChanhged += Refresh;
    }

    public void Refresh()
    {
        int index = StatusManager.Instance.GetStatType(StatType.attack);

        currentAtk.text = StatusManager.Instance.currentStatus.stats[index].value.ToString();
        currentMoney.text = StatusManager.Instance.currentStatus.money.ToString();
    }
}
