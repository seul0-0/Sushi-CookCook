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

    [Header("커서 이미지")]
    public Texture2D CursorTexture;  // 장착 시 사용할 커서 이미지
    public Vector2 CursorHotspot = Vector2.zero;
}
