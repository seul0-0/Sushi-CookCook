using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    [Header("Ui")]
    public TextMeshProUGUI currentAtk;
    public TextMeshProUGUI currentMoney;

    // === ������ ���� ������ �˷��ֱ� ===
    private void Start()
    {
        Refresh();

        StatusManager.Instance.OnMoneyChanged += Refresh;

        UpgradeButtonUi.OnStatusRefreshed += Refresh;

        WeaponSlotButton.OnWeaponChanhged += Refresh;

        SaveManager.OnDataLoaded += Refresh;
    }

    public void Refresh()
    {
        int index = StatusManager.Instance.GetStatType(StatType.attack);

        currentAtk.text = StatusManager.Instance.currentStatus.stats[index].value.ToString();
        currentMoney.text = StatusManager.Instance.currentStatus.money.ToString();
    }
}
