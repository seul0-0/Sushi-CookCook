using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string ItemName;
    public int ItemAttack;
    public int CriticalChance;
    public int ItemLevel;
    public Sprite ItemImage;
    public int price;
    public bool have;
}
