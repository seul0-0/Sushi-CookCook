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

    [Tooltip("레벨별 증가량, 0레벨은 기본값")]
    public Stats[] levelBonuses;

    // 특정 레벨에서 증가량 가져오기
    public Stats GetBonus(int level)
    {
        if (level < 0 || level >= levelBonuses.Length)
            return new Stats(); // 기본값 (0스탯)

        // 복사본 리턴 (원본 안전하게 유지)
        return new Stats(levelBonuses[level]);
    }
}