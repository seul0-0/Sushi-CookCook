using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : Singleton<EquipManager>
{
    [Header("��ũ���ͺ� ������Ʈ ���� (����)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    [Header("Ŭ�е� ���� ������")]
    public List<WeaponScriptableObject> weaponDatas = new();

    [Header("Current Equip")]
    public List<WeaponScriptableObject> currentWeapon = new();

    [Header("�������� ���")]
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
            ItemAttack.text = "����: " + data.ItemAttack;
        if (ItemCritical != null)
            ItemCritical.text = "�ؾ� : " + data.CriticalChance + "%";
    }
}
