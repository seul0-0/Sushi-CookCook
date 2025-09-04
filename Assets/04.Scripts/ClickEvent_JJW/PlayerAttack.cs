using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttack : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    public EnemyStatusTest enemyStatusTest;

    void Start()
    {
        // 공격 이벤트 구독
        EventManager.attackClick += OnAttack;

        // StatusManager에서 현재 플레이어 상태 가져오기
        if (StatusManager.Instance != null)
        {
            _playerStatus = StatusManager.Instance.currentStatus;
        }
    }

    private void OnAttack() // Player 클릭을 받아들인 후 처리
    {
        float damage = CalculateFinalAttack();
        enemyStatusTest.TakeDamage(damage);
        Debug.Log("플레이어 공격력: " + damage);

        // TODO: 여기서 EnemyManager 같은 곳으로 데미지를 전달하는 로직 필요
    }

    /// <summary>
    /// 최종 공격력 계산 (치명타 포함)
    /// </summary>
    private float CalculateFinalAttack()
    {
        float attack = _playerStatus.stats[(int)StatType.attack].value;
        float criticalChance = _playerStatus.stats[(int)StatType.critical].value;
        float criticalDamage = _playerStatus.stats[(int)StatType.criticalDamage].value;

        float random = Random.Range(1f, 100f);

        // 치명타 성공
        if (random <= criticalChance)
        {
            return attack * criticalDamage;
        }
        else
        {
            return attack;
        }
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제 (메모리 누수 방지)
        EventManager.attackClick -= OnAttack;
    }
}

