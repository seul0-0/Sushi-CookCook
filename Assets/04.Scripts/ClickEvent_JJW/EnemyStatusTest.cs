using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Enemy")]
public class EnemyStatusTest : ScriptableObject
{
     [Header("Status")]
    public float maxHp = 1000f;
    [HideInInspector] public float currentHp =1000f;
    public void Init()
    {
        currentHp = maxHp;
    }
    void OnEnable()   // ScriptableObject면 Awake 대신 OnEnable 사용
    {
        Init();   // 자동으로 초기화
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
    }

    public float GetHpRatio()
    {
        return currentHp / maxHp;
    }
}
