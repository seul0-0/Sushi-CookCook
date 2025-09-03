using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    [Header("UI ����")]
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemLv;
    public TextMeshProUGUI itemAttack;
    public TextMeshProUGUI itemCritical;

    public void SetSlot(WeaponScriptableObject data)
    {
        if (data == null) return;

        if (itemImage != null && data.ItemImage != null)
            itemImage.sprite = data.ItemImage;

        if (itemName != null)
            itemName.text = data.ItemName;

        if (itemLv != null)
            itemLv.text = "Lv " + data.ItemLevel;

        if (itemAttack != null)
            itemAttack.text = "���ݷ�: " + data.ItemAttack;

        if (itemCritical != null)
            itemCritical.text = "ġ��Ÿ Ȯ��: " + data.CriticalChance + "%";
    }
}