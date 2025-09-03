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

        UpgradeButtonUi.OnStatusRefreshed += Refresh;
    }

    public void Refresh()
    {
        int index = StatusManager.Instance.GetStatType(StatType.attack);

        currentAtk.text = StatusManager.Instance.status.stats[index].value.ToString();
        currentMoney.text = StatusManager.Instance.status.money.ToString();
    }
}
