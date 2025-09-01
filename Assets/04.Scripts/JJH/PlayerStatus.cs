using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PlayerStatus", menuName = "Player")]
public class PlayerStatus : ScriptableObject
{
    [Header("status")]                     // === �÷��̾� ���� ===
    public int attack;
    public int critical;

    [Header("recipt")]                     // === ��� ===
    public int money;
    public int luck;
}
