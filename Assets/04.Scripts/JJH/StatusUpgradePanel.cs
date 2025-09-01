using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    [SerializeField]
    private int upgrade_id = 5;                        // === 현재 panel의 번호 ===

    public PlayerStatus status;

    public Image panelicon;
    public TextMeshProUGUI nextUpgradeValue;
    public Button upgradeBtn;

    public Sprite[] staticons;                     // === icon을 미리 할당 ===

    private void Start()
    {
        upgradeBtn.onClick.AddListener(UpgradeStatus);
    }

    // ===   0    ,  1    ,     2       ,     3     ===
    // === 공격력 ,치명타, 치명타 데미지, 행운 스텟 ===
    public void SetPanel(int id)
    {
        upgrade_id = id;

        panelicon.sprite = staticons[id];

        nextUpgradeValue.text = id switch
        {
            0 => $"공격력 :\n {status.attack} => {status.CalculateNextAttackValue()}",
            1 => $"치명타 :\n {status.critical} => {status.CalculateNextCriticalValue()}",
            2 => $"치명타 데미지 :\n {status.criticalDamage} => {status.CalculateNextCriticalDamageValue()}",
            3 => $"행운 :\n {status.luck} => {status.CalculateNextLuckValue()}",
            _ => $"",
        };
    }

    public void UpgradeStatus()
    {
        // === 비용 계산 ===
        int upgradeCost;

        switch (upgrade_id)
        {
            case 0:
                upgradeCost = 1 * status.attackLevel;
                break;
            case 1:
                upgradeCost = 2 * status.criticalLevel;
                break;
            case 2:
                upgradeCost = 5 * status.criticalDamageLevel;
                break;
            case 3:
                upgradeCost = 10 * status.luckLevel;
                break;
            default:
                return; 
        }

        // === 돈이 부족할 경우 ===
        if (status.money < upgradeCost)
        {
            return;
        }

        // === 돈이 충분 할 경우 ===
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
    }
}
