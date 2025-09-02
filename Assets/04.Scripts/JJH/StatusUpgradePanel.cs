using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === �̺�Ʈ ȣ�� ===

    private int upgrade_id;                            // === ���� panel�� ��ȣ ===

    public PlayerStatus status;
    [Header("Info")]
    public Image panelicon;
    public TextMeshProUGUI upgradeName;

    [Header("Next")]
    public TextMeshProUGUI nextCost;
    public TextMeshProUGUI nextUpgradeValue;
    public Button upgradeBtn;

    public Sprite[] staticons;                         // === icon�� �̸� �Ҵ� ===

    private void Start()
    {
        upgradeBtn.onClick.AddListener(UpgradeStatus);
    }

    // ===   0    ,  1    ,     2       ,     3     ===
    // ===   ���� , �ؾ�  , �ؾ� ������, ��� ���� ===
    public void SetPanel(int id)
    {
        upgrade_id = id;

        panelicon.sprite = staticons[id];
        upgradeName.text = id switch
        {
            0 => " ����",
            1 => " �ؾ�",
            2 => " �ؾ� \n ��ȭ",
            3 => " ���",
            _ => "",
        };

        nextUpgradeValue.text = id switch
        {
            0 => $" {status.attack} =>\n {status.CalculateNextAttackValue()}",
            1 => $" {status.critical} =>\n {status.CalculateNextCriticalValue()}",
            2 => $" {status.criticalDamage} =>\n {status.CalculateNextCriticalDamageValue()}",
            3 => $" {status.luck} =>\n {status.CalculateNextLuckValue()}",
            _ => $"",
        };

        nextCost.text = id switch
        {
            0 => $"{1 * (status.attackLevel + 1)}",
            1 => $"{2 * (status.criticalLevel + 1)}",
            2 => $"{5 * (status.criticalDamageLevel + 1)}",
            3 => $"{10 * (status.luckLevel + 1)}",
            _ => $"",
        };
    }

    public void UpgradeStatus()
    {
        // === ��� ��� ===
        int upgradeCost;

        switch (upgrade_id)
        {
            case 0:
                upgradeCost = 1 * (status.attackLevel + 1);
                break;
            case 1:
                upgradeCost = 2 * (status.criticalLevel + 1);
                break;
            case 2:
                upgradeCost = 5 * (status.criticalDamageLevel + 1);
                break;
            case 3:
                upgradeCost = 10 * (status.luckLevel + 1);
                break;
            default:
                return; 
        }

        // === ���� ������ ��� ===
        if (status.money < upgradeCost)
        {
            return;
        }

        // === ���� ��� �� ��� ===
        switch (upgrade_id)
        {
            case 0:
                status.UpgradeAttackValue();
                status.ChangeMoney(-upgradeCost);
                break;
            case 1:
                status.UpgradeCriticalValue();
                status.ChangeMoney(-upgradeCost);
                break;
            case 2:
                status.UpgradeCriticalDamageValue();
                status.ChangeMoney(-upgradeCost);
                break;
            case 3:
                status.UpgradeLuckValue();
                status.ChangeMoney(-upgradeCost);
                break;
            default:
                break;
        }
        SetPanel(upgrade_id);

        // === ���� ���� â ���� ===
        OnStatusRefreshed?.Invoke();
    }
}
