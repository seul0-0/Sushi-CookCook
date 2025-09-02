using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    [Header("UI 연결")]
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
            itemAttack.text = "공격력: " + data.ItemAttack;

        if (itemCritical != null)
            itemCritical.text = "치명타 확률: " + data.CriticalChance + "%";
    }
}