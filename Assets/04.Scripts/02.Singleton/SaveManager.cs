using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerSaveData
{
    public string[] equippedWeaponNames;     // 슬롯별 무기 이름
    public int[] weaponEnhanceLevels;        // 슬롯별 강화 레벨
    public int gold;                         // 보유 골드
    public Dictionary<UpgradeType, int> upgradeLevels; // 업그레이드 상태
}

public static class SaveManager
{
    private const string PlayerKey = "PlayerSave";

    // 내부용: 실제 직렬화 저장
    public static void SavePlayer(PlayerDataM player)
    {
        var dto = new PlayerSaveData
        {
            equippedWeaponNames = player.GetEquippedWeaponNames(),
            weaponEnhanceLevels = player.GetWeaponEnhanceLevels(),
            upgradeLevels = new Dictionary<UpgradeType, int>(player.UpgradeLevels),
            gold = player.Gold
        };

        // JSON 직렬화 (Newtonsoft)
        string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        PlayerPrefs.SetString(PlayerKey, json);
        PlayerPrefs.Save();
    }

    // 내부용: 실제 불러오기
    public static PlayerSaveData LoadPlayer()
    {
        if (!PlayerPrefs.HasKey(PlayerKey))
            return null;

        string json = PlayerPrefs.GetString(PlayerKey);
        // JSON 역직렬화 (Newtonsoft)
        return JsonConvert.DeserializeObject<PlayerSaveData>(json);
    }



    // 통합: GameManager에서 바로 호출
    public static void SaveGame(PlayerDataM player)
    {
        SavePlayer(player);
        Debug.Log("게임 저장 완료!");
    }

    public static void LoadGame(PlayerDataM player, List<WeaponDataSO> weaponDB)
    {
        var saveData = LoadPlayer();
        if (saveData != null)
        {
            player.LoadFromSave(saveData, weaponDB);
            Debug.Log("게임 로드 완료!");
        }
        else
        {
            Debug.Log("저장 데이터 없음");
        }
    }
}
