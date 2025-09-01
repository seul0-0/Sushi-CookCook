using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    public PlayerStatus status;

    public TextMeshProUGUI currentAtk;
    public TextMeshProUGUI currentMoney;

    private void Start()
    {
        currentAtk.text = status.attack.ToString();
        currentMoney.text = status.money.ToString();
    }
}
