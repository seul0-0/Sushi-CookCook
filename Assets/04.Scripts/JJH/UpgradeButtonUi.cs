using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUi : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === ���� ����Ʈ ȣ�� ===

    public StatusUpgradePanel statusUpgradePanel;
    public PlayerStatus status;

    [Header("UpgradeBtn")]
    public TextMeshProUGUI nextCost;
    public Button upgradeBtn;

    // === ���� ��ư ��ȣ ===
    private int _buttonindex;
    // === ���׷��̵� ��� ===
    private int _upgradeCost;
    // === ���׷��̵� true�� ��� ���� ===
    private bool _isUpgradeReady;

    public void Start()
    {
        if(statusUpgradePanel != null && status != null)
        {
            _buttonindex = statusUpgradePanel.upgrade_id;

            SetButtonPanel();

            upgradeBtn.onClick.AddListener(UpgradeStatus);

            PlayerStatus.OnMoneyChanged += CheckCost;
        }

    }

    // === ���� ���� Ȯ�� ===
    public void SetButtonPanel()
    {
        _upgradeCost = status.CheckMoney(status.stats[_buttonindex].type);

        nextCost.text = $"{_upgradeCost}";

        CheckCost();
    }

    public void CheckCost()
    {
        // === ���� ������ ��� ===
        if (status.money < _upgradeCost)
        {
            upgradeBtn.image.color = Color.red;
            _isUpgradeReady = false;
            return;
        }

        upgradeBtn.image.color = Color.black;

        _isUpgradeReady = true;
    }
   
    public void UpgradeStatus()
    {
        if (_isUpgradeReady == false) { return; }

        // === ���׷��̵� ===
        status.UpgradeValue((status.stats[_buttonindex].type));

        status.ChangeMoneyValue(-_upgradeCost);

        // === ���׷��̵� üũ�� �ٽ� Ȱ��ȭ �ϱ� ���� ===
        _isUpgradeReady = false;

        // === �г� â ���� ===
        statusUpgradePanel.NextValue();

        // === ���� ��� ���� ===
        SetButtonPanel();

        // === ���� ���� â ���� ===
        OnStatusRefreshed?.Invoke();
    }
}
