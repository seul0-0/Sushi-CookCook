using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Attack,
    CriticalChance,
    CriticalDamage,
    AbilityAttack,
    GoldBonus
}

[CreateAssetMenu(fileName = "UpgradeTable", menuName = "Game/UpgradeTable")]
public class UpgradeTableSO : ScriptableObject
{
    public UpgradeType type;

    [Tooltip("������ ������, 0������ �⺻��")]
    public Stats[] levelBonuses;

    // Ư�� �������� ������ ��������
    public Stats GetBonus(int level)
    {
        if (level < 0 || level >= levelBonuses.Length)
            return new Stats(); // �⺻�� (0����)

        // ���纻 ���� (���� �����ϰ� ����)
        return new Stats(levelBonuses[level]);
    }
}