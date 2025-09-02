using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    [HideInInspector]
    public int upgrade_id;                            // === ���� panel�� ��ȣ ===

    public PlayerStatus status;

    [Header("Info")]
    public Image panelicon;
    public TextMeshProUGUI upgradeName;

    [Header("Next")]
    public TextMeshProUGUI nextUpgradeValue;

    public Sprite[] staticons;                         // === icon�� �̸� �Ҵ� ===

    // ===   0    ,  1    ,     2      ,     3     ,  4        ===
    // ===   ���� , �ؾ�  , �ؾ� ������, ��� ���� , �ڵ� ���� ===
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
            4 => " �ڵ� \n ����",
            _ => "",
        };

        NextValue();
    }

    public void NextValue()
    {
        StatType type = (StatType)upgrade_id;

        float currentValue = status.stats[upgrade_id].value;
        float nextValue = status.NextStatValue(type);

        nextUpgradeValue.text = $"{currentValue} =>\n {nextValue}";
    }
}
