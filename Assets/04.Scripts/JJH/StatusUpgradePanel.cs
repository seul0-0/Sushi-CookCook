using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpgradePanel : MonoBehaviour
{
    [HideInInspector]
    public int upgrade_id;                            // === ÇöÀç panelÀÇ ¹øÈ£ ===

    public PlayerStatus status;

    [Header("Info")]
    public Image panelicon;
    public TextMeshProUGUI upgradeName;

    [Header("Next")]
    public TextMeshProUGUI nextUpgradeValue;

    public Sprite[] staticons;                         // === iconÀ» ¹Ì¸® ÇÒ´ç ===

    // ===   0    ,  1    ,     2      ,     3     ,  4        ===
    // ===   ³»°ø , ¼Ø¾¾  , ¼Ø¾¾ µ¥¹ÌÁö, Çà¿î ½ºÅÝ , ÀÚµ¿ Á¶¸® ===
    public void SetPanel(int id)
    {
        upgrade_id = id;

        panelicon.sprite = staticons[id];

        upgradeName.text = id switch
        {
            0 => " ³»°ø",
            1 => " ¼Ø¾¾",
            2 => " ¼Ø¾¾ \n °­È­",
            3 => " Çà¿î",
            4 => " ÀÚµ¿ \n Á¶¸®",
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
