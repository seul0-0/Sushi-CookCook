using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private EquipManager _equipManager;

    [Header("클론된 무기 데이터")]
    public List<WeaponScriptableObject> weaponDatas = new();

    [Header("슬롯 연결")]
    public List<WeaponSlot> slots = new();

    public GameObject equipmentUI;

    [Header("Button")] // === 장비창 열고 닫기 ===
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
                var clone = Instantiate(weapon);  // ScriptableObject 복제
                weaponDatas.Add(clone);
            }
        }

        // === 버튼에 할당 ===
        openWindowButton.onClick.AddListener(OpenWindow);
        equipWindow.SetActive(false);

        UpdateUI();

        EquipGloveBtn();
    }

    // === 장비창 열고 닫기 ===
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
    // 강화 시 플레이어 골드 감소 추가해야함
    public void GloveUpgrade()
    {
        if(StatusManager.Instance.currentStatus.money < 10) 
            return;
        StatusManager.Instance.ChangeMoneyValue(-10);
        if (weaponDatas.Count > 0)
        {
            weaponDatas[0].ItemLevel++;
            weaponDatas[0].ItemAttack += 1;

            UpdateUI();

            if (_equipManager.currentWeapon[0] == weaponDatas[0])
            {
                SetCurrentWeapon(weaponDatas[0]);
            }
        }
    }
    public void RicePaddleUpgrade()
    {
        if (StatusManager.Instance.currentStatus.money < 10)
            return;
        StatusManager.Instance.ChangeMoneyValue(-10);
        if (weaponDatas.Count > 1)
        {
            weaponDatas[1].ItemLevel++;
            weaponDatas[1].ItemAttack += 2;

            UpdateUI();

            if (_equipManager.currentWeapon[0] == weaponDatas[1])
            {
                SetCurrentWeapon(weaponDatas[1]);
            }
        }
    }
    public void ChoppingBoardUpgrade()
    {
        if (StatusManager.Instance.currentStatus.money < 10)
            return;
        StatusManager.Instance.ChangeMoneyValue(-10);
        if (weaponDatas.Count > 2)
        {
            weaponDatas[2].ItemLevel++;
            weaponDatas[2].ItemAttack += 3;

            UpdateUI();

            if (_equipManager.currentWeapon[0] == weaponDatas[2])
            {
                SetCurrentWeapon(weaponDatas[2]);
            }
        }
    }
    public void KnifeUpgrade()
    {
        if (StatusManager.Instance.currentStatus.money < 10)
            return;
        StatusManager.Instance.ChangeMoneyValue(-10);
        if (weaponDatas.Count > 3)
        {
            weaponDatas[3].ItemLevel++;
            weaponDatas[3].ItemAttack += 3;

            UpdateUI();

            if (_equipManager.currentWeapon[0] == weaponDatas[3])
            {
                SetCurrentWeapon(weaponDatas[3]);
            }
        }
    }
    public void EquipGloveBtn()
    {
        _equipManager.currentWeapon.Clear();
        _equipManager.currentWeapon.Add(weaponDatas[0]);

        UpdateUI();

        SetCurrentWeapon(weaponDatas[0]);
    }
    public void EquipRicePaddleBtn()
    {
        _equipManager.currentWeapon.Clear();
        _equipManager.currentWeapon.Add(weaponDatas[1]);

        UpdateUI();

        SetCurrentWeapon(weaponDatas[1]);
    }
    public void EquipChoppingBoardBtn()
    {
        _equipManager.currentWeapon.Clear();
        _equipManager.currentWeapon.Add(weaponDatas[2]);

        UpdateUI();

        SetCurrentWeapon(weaponDatas[2]);
    }
    public void EquipKnifeBtn()
    {
        _equipManager.currentWeapon.Clear();
        _equipManager.currentWeapon.Add(weaponDatas[3]);

        UpdateUI();

        SetCurrentWeapon(weaponDatas[3]);
    }
    public void BackBtn()
    { 
        equipmentUI.SetActive(false); 
    }

    public void SetCurrentWeapon(WeaponScriptableObject data)
    {
        _equipManager.UpdateUiDisplay(data);
    }

    public void BuyRicePaddle()
    {
        if (StatusManager.Instance.currentStatus.money < weaponDatas[1].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-weaponDatas[1].price);
        // 이 버튼이 속한 부모(UnknownItem)를 찾아서 비활성화
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void BuyChoppingBoard()
    {
        if (StatusManager.Instance.currentStatus.money < weaponDatas[2].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-weaponDatas[2].price);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void BuyKnife()
    {
        if (StatusManager.Instance.currentStatus.money < weaponDatas[3].price)
            return;
        StatusManager.Instance.ChangeMoneyValue(-weaponDatas[3].price);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}

