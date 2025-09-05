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
    public List<WeaponSaveData> equippedWeapons; // ���� + ��ȭ����
    public List<WeaponSaveData> haveWeapons;     // === ������ �ִ� ���� + ��ȭ���� (��) ===
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
    public static event Action OnDataLoaded;      // === Ui���� ȣ�� ===

    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "playerSave.json");

    // ���ο�: ���� ����ȭ ����
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
                enhanceLevel = w.ItemLevel,   // ���⼭ ��ȭ�������� ���� ����!
                haveWeapon = w.have
            }).ToList(),
            haveWeapons = equip.weaponDatas.Select(w => new WeaponSaveData // === �������ִ� ���� ���� (��) ===
            {
                weaponName = w.ItemName,
                enhanceLevel = w.ItemLevel,  
                haveWeapon = w.have
            }).ToList()
        };

        string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        File.WriteAllText(SavePath, json);
        Debug.Log($"���� ���� �Ϸ�! ���: {SavePath}");
    }

    // ���ο�: ���� �ҷ�����
    public static void Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("���� ������ �����ϴ�.");
            return;
        }
        string json = File.ReadAllText(SavePath);
        var dto = JsonConvert.DeserializeObject<PlayerSaveData>(json);

        // StatusManager ����
        var status = StatusManager.Instance.currentStatus;
        status.money = dto.gold;

        foreach (var s in dto.stats)
        {
            int index = StatusManager.Instance.GetStatType(s.type);
            status.stats[index].value = s.value;
            status.stats[index].level = s.level;
        }

        // EquipManager ����
        var equip = EquipManager.Instance;
        equip.currentWeapon.Clear();

        foreach (var w in dto.equippedWeapons)
        {
            var weapon = equip.originalWeaponDatas.FirstOrDefault(x => x.ItemName == w.weaponName);
            if (weapon != null)
            {
                weapon.ItemLevel = w.enhanceLevel; // ����� ��ȭ���� ����
                equip.currentWeapon.Add(weapon);
            }
        }
        // === �������ִ� ������ ���� ===
        foreach (var w in dto.haveWeapons)
        {
            var weapon = equip.weaponDatas.FirstOrDefault(x => x.ItemName == w.weaponName);
            if (weapon != null)
            {
                weapon.ItemLevel = w.enhanceLevel; // ����� ��ȭ���� ����
                weapon.have = w.haveWeapon;        // === ���� ���� ===
            }
        }

        // UI ����
        if (equip.currentWeapon.Count > 0)
            equip.UpdateUiDisplay(equip.currentWeapon[0]);
        Debug.Log($"���� �ε� �Ϸ�! ���: {SavePath}");

        OnDataLoaded?.Invoke();
    }
}
