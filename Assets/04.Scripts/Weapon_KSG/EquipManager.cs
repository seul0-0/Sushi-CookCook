using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public PlayerStatus playerStatus;
    [Header("스크립터블 오브젝트 연결 (원본)")]
    public List<WeaponScriptableObject> originalWeaponDatas = new();

    private void Start()
    {
        if(StatusManager.Instance != null)
        {
            playerStatus = StatusManager.Instance.status;
        }
    }
}
