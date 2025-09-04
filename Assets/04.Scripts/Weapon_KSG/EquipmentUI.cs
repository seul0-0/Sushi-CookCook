using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private EquipManager _equipManager;

    [Header("Ŭ�е� ���� ������")]
    public List<WeaponScriptableObject> weaponDatas = new();

    [Header("���� ����")]
    public List<WeaponSlot> slots = new();

    public GameObject equipmentUI;

    [Header("Button")] // === ���â ���� �ݱ� ===
    public Button openWindowButton;
    public GameObject equipWindow;
    private bool _isClick;

    void Start()
    {
        if(EquipManager.Instance != null)
        {
            _equipManager = EquipManager.Instance;
        }

        weaponDatas.Clear();

        foreach (var weapon in _equipManager.originalWeaponDatas)
        {
            if (weapon != null)
            {
                var clone = Instantiate(weapon);  // ScriptableObject ����
                weaponDatas.Add(clone);
            }
        }

        // === ��ư�� �Ҵ� ===
        openWindowButton.onClick.AddListener(OpenWindow);

        if(_equipManager.currentWeapon != null)
        {
            equipWindow.SetActive(false);
        }

        EquipManager.Instance.currentWeapon.Add(weaponDatas[0]);
        
        UpdateUI();

        SetCurrentWeapon(weaponDatas[0]);
    }

    // === ���â ���� �ݱ� ===
    public void OpenWindow()
    {
        _isClick = !_isClick;
        
        equipWindow.SetActive(_isClick);
    }

    public void UpdateUI()
    {
        int count = Mathf.Min(slots.Count, weaponDatas.Count);

        for (int i = 0; i < count; i++)
        {
            slots[i].SetSlot(weaponDatas[i]);
        }
    }

    public void BackBtn()
    { 
        equipmentUI.SetActive(false); 
    }

    public void SetCurrentWeapon(WeaponScriptableObject data)
    {
        _equipManager.UpdateUiDisplay(data);

        StatusManager.Instance.currentStatus.stats[(int)StatType.attack].value += _equipManager.currentWeapon[0].ItemAttack;
        StatusManager.Instance.currentStatus.stats[(int)StatType.critical].value += _equipManager.currentWeapon[0].CriticalChance;

        WeaponSlotButton.OnWeaponChanhged?.Invoke();
    }
}

