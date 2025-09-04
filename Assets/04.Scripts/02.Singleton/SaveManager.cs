using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerSaveData
{
    public string[] equippedWeaponNames;     // ���Ժ� ���� �̸�
    public int[] weaponEnhanceLevels;        // ���Ժ� ��ȭ ����
    public int gold;                         // ���� ���
    public Dictionary<UpgradeType, int> upgradeLevels; // ���׷��̵� ����
}

public static class SaveManager
{
    private const string PlayerKey = "PlayerSave";

    // ���ο�: ���� ����ȭ ����
    public static void SavePlayer(PlayerDataM player)
    {
        var dto = new PlayerSaveData
        {
            equippedWeaponNames = player.GetEquippedWeaponNames(),
            weaponEnhanceLevels = player.GetWeaponEnhanceLevels(),
            upgradeLevels = new Dictionary<UpgradeType, int>(player.UpgradeLevels),
            gold = player.Gold
        };

        // JSON ����ȭ (Newtonsoft)
        string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
        PlayerPrefs.SetString(PlayerKey, json);
        PlayerPrefs.Save();
    }

    // ���ο�: ���� �ҷ�����
    public static PlayerSaveData LoadPlayer()
    {
        if (!PlayerPrefs.HasKey(PlayerKey))
            return null;

        string json = PlayerPrefs.GetString(PlayerKey);
        // JSON ������ȭ (Newtonsoft)
        return JsonConvert.DeserializeObject<PlayerSaveData>(json);
    }



    // ����: GameManager���� �ٷ� ȣ��
    public static void SaveGame(PlayerDataM player)
    {
        SavePlayer(player);
        Debug.Log("���� ���� �Ϸ�!");
    }

    public static void LoadGame(PlayerDataM player, List<WeaponDataSO> weaponDB)
    {
        var saveData = LoadPlayer();
        if (saveData != null)
        {
            player.LoadFromSave(saveData, weaponDB);
            Debug.Log("���� �ε� �Ϸ�!");
        }
        else
        {
            Debug.Log("���� ������ ����");
        }
    }
}
