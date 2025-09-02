using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    public PlayerStatus status;

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
        int index = status.GetStatType(StatType.attack);

        currentAtk.text = status.stats[index].value.ToString();
        currentMoney.text = status.money.ToString();
    }
}
