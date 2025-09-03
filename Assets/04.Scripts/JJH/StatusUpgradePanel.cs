using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    [HideInInspector]
    public int upgrade_id;                            // === ���� panel�� ��ȣ ===

    [Header("Info")]
    public Image panelicon;
    public TextMeshProUGUI upgradeName;

    [Header("Next")]
    public TextMeshProUGUI nextUpgradeValue;

    public Sprite[] staticons;                         // === icon�� �̸� �Ҵ� ===

    // ===   0    ,  1    ,     2    ,     3     ,  4        ===
    // ===   ���� , �ؾ�  , �ؾ� ��ȭ, ��� ���� , �ڵ� ���� ===
    public void SetPanel(int id)
    {
        upgrade_id = id;

        panelicon.sprite = staticons[id];

        upgradeName.text = StatusManager.Instance.status.stats[id].name;

        NextValue();
    }

    public void NextValue()
    {
        StatType type = (StatType)upgrade_id;

        float currentValue = StatusManager.Instance.status.stats[upgrade_id].value;
        float nextValue = StatusManager.Instance.status.NextStatValueDisplay(type);

        nextUpgradeValue.text = $"{currentValue} =>\n {nextValue}";
    }
}
