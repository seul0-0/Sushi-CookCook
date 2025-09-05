using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoAttack : MonoBehaviour
{
    public Toggle autoAttackToggle;
    private Coroutine autoAttackRoutine;
    public EnemyStatusTest enemyStatusTest;
    private PlayerStatus _playerStatus;

    void Start()
    {
        // 토글 이벤트 연결
        autoAttackToggle.onValueChanged.AddListener(OnToggleChanged);
        EventManager.autoAttackClick += OnAttack;
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

    void OnToggleChanged(bool isOn)
    {
        if (isOn && StatusManager.Instance.currentStatus.stats[4].level > 0)
        {
            // 자동공격 시작
            autoAttackRoutine = StartCoroutine(AutoAttackCoroutine());
        }
        else
        {
            // 자동공격 중지
            if (autoAttackRoutine != null)
            {
                StopCoroutine(autoAttackRoutine);
                autoAttackRoutine = null;
            }
        }
    }
    private float CalculateFinalAttack()
    {
        float autoAttack = _playerStatus.stats[(int)StatType.autoattack].value;
        float criticalChance = _playerStatus.stats[(int)StatType.critical].value;
        float criticalDamage = _playerStatus.stats[(int)StatType.criticalDamage].value;

        float random = Random.Range(1f, 100f);

        // 치명타 성공
        if (random <= criticalChance)
        {
            return autoAttack * criticalDamage;
        }
        else
        {
            return autoAttack;
        }
    }

    IEnumerator AutoAttackCoroutine() // 자동공격 실행 부분
    {
        while (true)
        {
            float attackInterval = Mathf.Max(0.5f, 2f - _playerStatus.stats[(int)StatType.autoattack].level * 0.2f);
            yield return new WaitForSeconds(attackInterval);

            EventManager.autoAttackClick?.Invoke();
            Debug.Log("자동공격 실행");
        }
    }
}
