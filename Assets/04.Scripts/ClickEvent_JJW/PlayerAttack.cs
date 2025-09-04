using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public EnemyStatusTest enemyStatus;

    void OnEnable()
    {
        EventManager.attackClick += OnAttack;
    }

    void OnDisable()
    {
        EventManager.attackClick -= OnAttack;
    }

    void OnAttack()
    {
        float damage = 0.1f;
        enemyStatus.TakeDamage(damage);

        if (enemyStatus.currentHp <= 0)
        {
            Debug.Log("Enemy Down");
        }
    }
}
