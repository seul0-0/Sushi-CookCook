using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : Singleton<EquipManager>
{
    [HideInInspector] public PlayerStatus playerStatus;

    [Header("��ũ���ͺ� ������Ʈ ���� (����)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    [Header("Ŭ�е� ���� ������")]
    public List<WeaponScriptableObject> weaponDatas = new();

    [Header("Current Equip")]
    public List<WeaponScriptableObject> currentWeapon = new();

    // Ŀ�� ����� �̺�Ʈ
    public static System.Action<Texture2D, Vector2> OnCursorChanged;

    [Header("�������� ���")]
    public Image ItemImg;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAttack;
    public TextMeshProUGUI ItemCritical;

    private void Start()
    {
        // PlayerStatus 연결
        if (StatusManager.Instance != null)
            playerStatus = StatusManager.Instance.currentStatus;

        // 기본 장착 아이템 세팅 (예: 슬롯0 = 장갑)
        if (originalWeaponDatas.Count > 0)
        {
            // currentWeapon 초기화
            while (currentWeapon.Count < originalWeaponDatas.Count)
                currentWeapon.Add(null);

            EquipItem(originalWeaponDatas[0], 0);
        }
    }
    public void EquipItem(WeaponScriptableObject data, int slotIndex)
    {
        Debug.Log($"[EquipManager] EquipItem 호출: {data.name}");
        if (data == null) return;

        if (slotIndex < currentWeapon.Count)
            currentWeapon[slotIndex] = data;

        UpdateUiDisplay(data);

        if (data.CursorTexture != null)
        {
            OnCursorChanged?.Invoke(data.CursorTexture, data.CursorHotspot);
            Debug.Log($"[EquipManager] 커서 변경 호출: {data.name}, Texture: {data.CursorTexture.name}");
        }
        else
        {
            OnCursorChanged?.Invoke(null, Vector2.zero);
            Debug.Log("[EquipManager] 커서 기본으로 변경");
        }
    }
    public void UpdateUiDisplay(WeaponScriptableObject data)
    {
        Debug.Log("아이템 변경 ");
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
