using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public PlayerStatus playerStatus;
    [Header("��ũ���ͺ� ������Ʈ ���� (����)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    private void Start()
    {
        if(StatusManager.Instance != null)
        {
            playerStatus = StatusManager.Instance.status;
        }
    }
}
