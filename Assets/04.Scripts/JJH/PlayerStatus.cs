using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("status")]                     // === 플레이어 스텟 ===
    public int attack;
    public int critical;

    [Header("recipt")]                     // === 결산 ===
    public int money;
    public int luck;
}
