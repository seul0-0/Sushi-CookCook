using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Image enemyHp;
    public PlayerStatus playerStatus;

    void Start()
    {
        EventManager.attackClick += OnAttack;
    }
    void OnAttack() // Player 클릭을 받아들인 후 처리
    {
        float damage = 0.1f;
        Debug.Log("플레이어 공격력: " + damage);

        enemyHp.fillAmount -= damage;
        if (enemyHp.fillAmount == 0)
        {
            Debug.Log("Enemy Down");
            // 후에 적 data가 바뀌는 로직을 추가하면 된다.
        }
    }
}
