using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // ===   0    ,  1    ,     2      ,     3     ===
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

        NextValue();
    }

    public void NextValue()
    {
        nextUpgradeValue.text = upgrade_id switch
        {
            0 => $" {status.attack} =>\n {status.CalculateNextAttackValue()}",
            1 => $" {status.critical} =>\n {status.CalculateNextCriticalValue()}",
            2 => $" {status.criticalDamage} =>\n {status.CalculateNextCriticalDamageValue()}",
            3 => $" {status.luck} =>\n {status.CalculateNextLuckValue()}",
            _ => $"",
        };
    }
}
