using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    attack,
    critical,
    criticalDamage,
    luck,
    autoattack
}

[Serializable]
public struct StatData
{
    public StatType type;
    public string name;
    public float value;
    public int level;
}

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("Status")]                     // === �÷��̾� ���� ===
    public StatData[] stats;

    [Header("Recipt")]                    
    public int money = 10;

    [Header("Sprite")]
    public Sprite[] staticons;             // === icon�� �̸� �Ҵ� ===
}
