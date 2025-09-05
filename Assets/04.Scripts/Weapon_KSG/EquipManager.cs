using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : Singleton<EquipManager>
{
    [Header("스크립터블 오브젝트 연결 (원본)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    [Header("클론된 무기 데이터")]
    public List<WeaponScriptableObject> weaponDatas = new();

    [Header("Current Equip")]
    public List<WeaponScriptableObject> currentWeapon = new();

    [Header("장착중인 장비")]
    public Image ItemImg;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAttack;
    public TextMeshProUGUI ItemCritical;

    public void UpdateUiDisplay(WeaponScriptableObject data)
    {
        if (data == null) return;
        if (ItemImg != null && data.ItemImage != null)
            ItemImg.sprite = data.ItemImage;
        if (ItemName != null)
            ItemName.text = data.ItemName;
        if (ItemAttack != null)
            ItemAttack.text = "내공: " + data.ItemAttack;
        if (ItemCritical != null)
            ItemCritical.text = "솜씨 : " + data.CriticalChance + "%";
    }
}
