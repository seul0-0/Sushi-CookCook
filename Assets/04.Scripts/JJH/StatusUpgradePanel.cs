using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    public PlayerStatus status;

    public Image panelicon;
    public TextMeshProUGUI nextUpgradeValue;
    public Button upgradeBtn;

    public Sprite[] staticons;                     // === icon�� �̸� �Ҵ� ===

    private void Start()
    {
        upgradeBtn.onClick.AddListener(Upgrade);
    }

    // === ���ݷ� ,ġ��Ÿ, ġ��Ÿ ������, ��� ���� ===
    public void SetPanel(int id)
    {
        panelicon.sprite = staticons[id];

        nextUpgradeValue.text = id switch
        {
            0 => $"���ݷ� :\n {status.attack} => {status.UpgradeAttackValue()}",
            1 => $"ġ��Ÿ :\n {status.critical} => {status.UpgradeCriticalValue()}",
            2 => $"ġ��Ÿ ������ :\n {status.criticalDamage} => {status.UpgradeCriticalDamageValue()}",
            3 => $"��� :\n {status.luck} => {status.UpgradeLuckValue()}",
            _ => $"",
        };
    }

    public void Upgrade()
    {
        Debug.Log("����");
    }
}
