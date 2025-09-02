using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUi : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === 델리 게이트 호출 ===

    public StatusUpgradePanel statusUpgradePanel;

    [Header("UpgradeBtn")]
    public TextMeshProUGUI nextCost;
    public Button upgradeBtn;

    // === 업그레이드 비용 ===
    private int _upgradeCost;
    // === 업그레이드 true일 경우 가능 ===
    private bool _isUpgradeReady;

    public void Start()
    {
        if(statusUpgradePanel != null)
        {
            SetButtonPanel();

            upgradeBtn.onClick.AddListener(UpgradeStatus);
        }

        OnStatusRefreshed += CheckCost;
    }

    // === 다음 레벨 확인 ===
    public void SetButtonPanel()
    {
        nextCost.text = statusUpgradePanel.upgrade_id switch
        {
            0 => $"{1 * (statusUpgradePanel.status.attackLevel + 1)}",
            1 => $"{2 * (statusUpgradePanel.status.criticalLevel + 1)}",
            2 => $"{5 * (statusUpgradePanel.status.criticalDamageLevel + 1)}",
            3 => $"{10 * (statusUpgradePanel.status.luckLevel + 1)}",
            4 => $"{100 * (statusUpgradePanel.status.autoAttackLevel + 1)}",
            _ => $"",
        };

        CheckCost();
    }

    public void CheckCost()
    {
        switch (statusUpgradePanel.upgrade_id)
        {
            case 0:
                _upgradeCost = 1 * (statusUpgradePanel.status.attackLevel + 1);
                break;
            case 1:
                _upgradeCost = 2 * (statusUpgradePanel.status.criticalLevel + 1);
                break;
            case 2:
                _upgradeCost = 5 * (statusUpgradePanel.status.criticalDamageLevel + 1);
                break;
            case 3:
                _upgradeCost = 10 * (statusUpgradePanel.status.luckLevel + 1);
                break;
            case 4:
                _upgradeCost = 100 * (statusUpgradePanel.status.autoAttackLevel + 1);
                break;
            default:
                return;
        }

        // === 돈이 부족할 경우 ===
        if (statusUpgradePanel.status.money < _upgradeCost)
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

        switch (statusUpgradePanel.upgrade_id)
        {
            case 0:
                statusUpgradePanel.status.UpgradeAttackValue();
                statusUpgradePanel.status.ChangeMoney(-_upgradeCost);
                break;
            case 1:
                statusUpgradePanel.status.UpgradeCriticalValue();
                statusUpgradePanel.status.ChangeMoney(-_upgradeCost);
                break;
            case 2:
                statusUpgradePanel.status.UpgradeCriticalDamageValue();
                statusUpgradePanel.status.ChangeMoney(-_upgradeCost);
                break;
            case 3:
                statusUpgradePanel.status.UpgradeLuckValue();
                statusUpgradePanel.status.ChangeMoney(-_upgradeCost);
                break;
            case 4:
                statusUpgradePanel.status.UpgradeAutoAttackValue();
                statusUpgradePanel.status.ChangeMoney(-_upgradeCost);
                break;
            default:
                break;
        }

        _isUpgradeReady = false;

        // === 패널 창 갱신 ===
        statusUpgradePanel.NextValue();

        // === 다음 비용 갱신 ===
        SetButtonPanel();

        // === 현재 스텟 창 갱신 ===
        OnStatusRefreshed?.Invoke();
    }
}
