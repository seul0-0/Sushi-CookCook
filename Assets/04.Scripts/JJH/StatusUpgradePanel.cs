using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    [HideInInspector]
    public int upgrade_id;                            // === ÇöÀç panelÀÇ ¹øÈ£ ===

    [Header("Info")]
    public Image panelicon;
    public TextMeshProUGUI upgradeName;

    [Header("Next")]
    public TextMeshProUGUI nextUpgradeValue;

    // ===   0    ,  1    ,     2    ,     3     ,  4        ===
    // ===   ³»°ø , ¼Ø¾¾  , ¼Ø¾¾ °­È­, Çà¿î ½ºÅÝ , ÀÚµ¿ Á¶¸® ===
    public void SetPanel(int id)
    {
        upgrade_id = id;

        panelicon.sprite = StatusManager.Instance.currentStatus.staticons[id];

        upgradeName.text = StatusManager.Instance.currentStatus.stats[id].name;

        NextValue();
    }

    public void NextValue()
    {
        StatType type = (StatType)upgrade_id;

        float currentValue = StatusManager.Instance.currentStatus.stats[upgrade_id].value;
        float nextValue = StatusManager.Instance.NextStatValueDisplay(type);

        nextUpgradeValue.text = $"{currentValue} =>\n {nextValue}";
    }
}
