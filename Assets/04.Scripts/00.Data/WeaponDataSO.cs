using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponName;

    [Tooltip("무기 강화 단계별 스탯 (0레벨 = 기본)")]
    public Stats[] enhanceStats;

    // 특정 강화 레벨에서 스탯 반환
    public Stats GetStatsByEnhanceLevel(int level)
    {
        if (enhanceStats == null || enhanceStats.Length == 0)
            return new Stats(); // 기본값

        if (level < 0) level = 0;
        if (level >= enhanceStats.Length) level = enhanceStats.Length - 1;

        // 깊은 복사로 반환 (외부에서 수정 못하게)
        return new Stats(enhanceStats[level]);
    }
}