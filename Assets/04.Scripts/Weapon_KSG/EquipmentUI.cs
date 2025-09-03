using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public PlayerStatus playerStatus;
    [Header("스크립터블 오브젝트 연결 (원본)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new List<WeaponScriptableObject>();

    [Header("클론된 무기 데이터")]
    public List<WeaponScriptableObject> weaponDatas = new List<WeaponScriptableObject>();

    [Header("슬롯 연결")]
    public List<WeaponSlot> slots = new List<WeaponSlot>();

    public GameObject equipmentUI;

    [Header("장착중인 장비")]
    public Image ItemImg;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAttack;
    public TextMeshProUGUI ItemCritical;



    void Start()
    {
        weaponDatas.Clear();
        foreach (var weapon in originalWeaponDatas)
        {
            if (weapon != null)
            {
                var clone = Instantiate(weapon);  // ScriptableObject 복제
                weaponDatas.Add(clone);
            }
        }
        UpdateUI();
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
        }
    }
    public void EquipGloveBtn()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            weaponDatas[i].isEquipped = false;
        }
        if (weaponDatas.Count > 0)
        {
            weaponDatas[0].isEquipped = true;
            UpdateUI();
        }
        SetCurrentWeapon(weaponDatas[0]);
    }
    public void EquipRicePaddleBtn()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            weaponDatas[i].isEquipped = false;
        }
        if (weaponDatas.Count > 1)
        {
            weaponDatas[1].isEquipped = true;
            UpdateUI();
        }
        SetCurrentWeapon(weaponDatas[1]);
    }
    public void EquipChoppingBoardBtn()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            weaponDatas[i].isEquipped = false;
        }
        if (weaponDatas.Count > 2)
        {
            weaponDatas[2].isEquipped = true;
            UpdateUI();
        }
        SetCurrentWeapon(weaponDatas[2]);
    }
    public void EquipKnifeBtn()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            weaponDatas[i].isEquipped = false;
        }
        if (weaponDatas.Count > 3)
        {
            weaponDatas[3].isEquipped = true;
            UpdateUI();
        }
        SetCurrentWeapon(weaponDatas[3]);
    }
    public void BackBtn()
    { 
        equipmentUI.SetActive(false); 
    }

    public void SetCurrentWeapon(WeaponScriptableObject data)
    {
        if (data == null) return;
        if (ItemImg != null && data.ItemImage != null)
            ItemImg.sprite = data.ItemImage;
        if (ItemName != null)
            ItemName.text = data.ItemName;
        if (ItemAttack != null)
            ItemAttack.text = "공격력: " + data.ItemAttack;
        if (ItemCritical != null)
            ItemCritical.text = "치명타 확률: " + data.CriticalChance + "%";
    }

    public void BuyRicePaddle()
    {
        if (StatusManager.Instance.currentStatus.money < 30)
            return;
        StatusManager.Instance.ChangeMoneyValue(-30);
        // 이 버튼이 속한 부모(UnknownItem)를 찾아서 비활성화
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void BuyChoppingBoard()
    {
        if (StatusManager.Instance.currentStatus.money < 60)
            return;
        StatusManager.Instance.ChangeMoneyValue(-60);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void BuyKnife()
    {
        if (StatusManager.Instance.currentStatus.money < 60)
            return;
        StatusManager.Instance.ChangeMoneyValue(-60);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}

