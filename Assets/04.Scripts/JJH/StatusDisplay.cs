using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    public PlayerStatus status;

    public TextMeshProUGUI currentAtk;
    public TextMeshProUGUI currentMoney;

    // === 현재의 스텟 정보를 알려주기 ===
    private void Start()
    {
        currentAtk.text = status.attack.ToString();
        currentMoney.text = status.money.ToString();
    }
}
