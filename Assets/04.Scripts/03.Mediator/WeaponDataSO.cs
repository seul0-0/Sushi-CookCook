using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponName;

    [Tooltip("���� ��ȭ �ܰ躰 ���� (0���� = �⺻)")]
    public Stats[] enhanceStats;

    // Ư�� ��ȭ �������� ���� ��ȯ
    public Stats GetStatsByEnhanceLevel(int level)
    {
        if (enhanceStats == null || enhanceStats.Length == 0)
            return new Stats(); // �⺻��

        if (level < 0) level = 0;
        if (level >= enhanceStats.Length) level = enhanceStats.Length - 1;

        // ���� ����� ��ȯ (�ܺο��� ���� ���ϰ�)
        return new Stats(enhanceStats[level]);
    }
}