using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : Singleton<EquipManager>
{
    [HideInInspector] public PlayerStatus playerStatus;

    [Header("Original")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    [Header("Clone")]
    public List<WeaponScriptableObject> weaponDatas = new();

    [Header("Current Equip")]
    public List<WeaponScriptableObject> currentWeapon = new();

    // Ä¿¼­ Ã¼ÀÎÁö
    public static System.Action<Texture2D, Vector2> OnCursorChanged;

    [Header("cuurrent")]
    public Image ItemImg;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAttack;
    public TextMeshProUGUI ItemCritical;

    private void Start()
    {
        if (StatusManager.Instance != null)
            playerStatus = StatusManager.Instance.currentStatus;

        if (originalWeaponDatas.Count > 0)
        {
            while (currentWeapon.Count < originalWeaponDatas.Count)
                currentWeapon.Add(null);

            EquipItem(originalWeaponDatas[0], 0);
        }
    }

    public void EquipItem(WeaponScriptableObject data, int slotIndex)
    {
        if (data == null) return;

        if (slotIndex < currentWeapon.Count)
            currentWeapon[slotIndex] = data;

        UpdateUiDisplay(data);

        if (data.CursorTexture != null)
        {
            OnCursorChanged?.Invoke(data.CursorTexture, data.CursorHotspot);
        }
        else
        {
            OnCursorChanged?.Invoke(null, Vector2.zero);
        }
    }

    public void UpdateUiDisplay(WeaponScriptableObject data)
    {
        if (data == null) return;
        if (ItemImg != null && data.ItemImage != null)
            ItemImg.sprite = data.ItemImage;
        if (ItemName != null)
            ItemName.text = data.ItemName;
        if (ItemAttack != null)
            ItemAttack.text = "³»°ø : " + data.ItemAttack;
        if (ItemCritical != null)
            ItemCritical.text = "¼Ø¾¾ : " + data.CriticalChance + "%";
    }
}
