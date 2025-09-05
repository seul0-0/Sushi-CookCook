using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[Serializable]
public class PlayerSaveData
{
    public int gold;
    public List<StatSaveData> stats;
    public List<WeaponSaveData> equippedWeapons; // 무기 + 강화레벨
    public List<WeaponSaveData> haveWeapons;     // === 가지고 있는 무기 + 강화레벨 (조) ===
}

[Serializable]
public class StatSaveData
{
    public StatType type;
    public float value;
    public int level;
}

[Serializable]
public class WeaponSaveData
{
    public string weaponName;
    public int enhanceLevel;
    public bool haveWeapon;
}

public static class SaveManager
{
    public static event Action OnDataLoaded;      // === Ui갱신 호출 ===

    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "playerSave.json");

    // 내부용: 실제 직렬화 저장
    public static void Save()
    {
        var status = StatusManager.Instance.currentStatus;
        var equip = EquipManager.Instance;

        var dto = new PlayerSaveData
        {
            gold = status.money,
            stats = status.stats.Select(s => new StatSaveData
            {
                type = s.type,
                value = s.value,
                level = s.level
            }).ToList(),
            equippedWeapons = equip.currentWeapon.Select(w => new WeaponSaveData
            {
                weaponName = w.ItemName,
                enhanceLevel = w.ItemLevel,   // 여기서 강화레벨까지 같이 저장!
                haveWeapon = w.have
            }).ToList(),
            haveWeapons = equip.weaponDatas.Select(w => new WeaponSaveData // === 가지고있는 무기 저장 (조) ===
            {
                weaponName = w.ItemName,
                enhanceLevel = w.ItemLevel,  
                haveWeapon = w.have
            }).ToList()
        };

        string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        File.WriteAllText(SavePath, json);
        Debug.Log($"게임 저장 완료! 경로: {SavePath}");
    }

    // 내부용: 실제 불러오기
    public static void Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("저장 파일이 없습니다.");
            return;
        }
        string json = File.ReadAllText(SavePath);
        var dto = JsonConvert.DeserializeObject<PlayerSaveData>(json);

        // StatusManager 복원
        var status = StatusManager.Instance.currentStatus;
        status.money = dto.gold;

        foreach (var s in dto.stats)
        {
            int index = StatusManager.Instance.GetStatType(s.type);
            status.stats[index].value = s.value;
            status.stats[index].level = s.level;
        }

        // EquipManager 복원
        var equip = EquipManager.Instance;
        equip.currentWeapon.Clear();

        foreach (var w in dto.equippedWeapons)
        {
            var weapon = equip.originalWeaponDatas.FirstOrDefault(x => x.ItemName == w.weaponName);
            if (weapon != null)
            {
                weapon.ItemLevel = w.enhanceLevel; // 저장된 강화레벨 적용
                equip.currentWeapon.Add(weapon);
            }
        }
        // === 가지고있던 장비들의 정보 ===
        foreach (var w in dto.haveWeapons)
        {
            var weapon = equip.weaponDatas.FirstOrDefault(x => x.ItemName == w.weaponName);
            if (weapon != null)
            {
                weapon.ItemLevel = w.enhanceLevel; // 저장된 강화레벨 적용
                weapon.have = w.haveWeapon;        // === 구매 내역 ===
            }
        }

        // UI 갱신
        if (equip.currentWeapon.Count > 0)
            equip.UpdateUiDisplay(equip.currentWeapon[0]);
        Debug.Log($"게임 로드 완료! 경로: {SavePath}");

        OnDataLoaded?.Invoke();
    }
}
