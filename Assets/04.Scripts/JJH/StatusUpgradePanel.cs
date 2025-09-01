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

    public Sprite[] staticons;                     // === icon을 미리 할당 ===

    private void Start()
    {
        upgradeBtn.onClick.AddListener(Upgrade);
    }

    // === 공격력 ,치명타, 치명타 데미지, 행운 스텟 ===
    public void SetPanel(int id)
    {
        panelicon.sprite = staticons[id];

        nextUpgradeValue.text = id switch
        {
            0 => $"공격력 :\n {status.attack} => {status.UpgradeAttackValue()}",
            1 => $"치명타 :\n {status.critical} => {status.UpgradeCriticalValue()}",
            2 => $"치명타 데미지 :\n {status.criticalDamage} => {status.UpgradeCriticalDamageValue()}",
            3 => $"행운 :\n {status.luck} => {status.UpgradeLuckValue()}",
            _ => $"",
        };
    }

    public void Upgrade()
    {
        Debug.Log("업글");
    }
}
