using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        switch (upgrade_id)
        {
            case 0:
                status.UpgradeAttackValue();
                status.ChangeMoney(-1 * status.attackLevel);
                break;
            case 1:
                status.UpgradeCriticalValue();
                status.ChangeMoney(-2 * status.criticalLevel);
                break;
            case 2:
                status.UpgradeCriticalDamageValue();
                status.ChangeMoney(-5 * status.criticalDamageLevel);
                break;
            case 3:
                status.UpgradeLuckValue();
                status.ChangeMoney(-10 * status.luckLevel);
                break;
            default:
                break;
        }
        SetPanel(upgrade_id);
    }
}
