using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : Singleton<EquipManager>
{
    [HideInInspector]
    public PlayerStatus playerStatus;

    [Header("��ũ���ͺ� ������Ʈ ���� (����)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    [Header("Current Equip")]
    public List<WeaponScriptableObject> currentWeapon = new();

    [Header("�������� ���")]
    public Image ItemImg;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAttack;
    public TextMeshProUGUI ItemCritical;

    private void Start()
    {
        if(StatusManager.Instance != null)
        {
            playerStatus = StatusManager.Instance.currentStatus;
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
            ItemAttack.text = "����: " + data.ItemAttack;
        if (ItemCritical != null)
            ItemCritical.text = "�ؾ� : " + data.CriticalChance + "%";
    }
}
