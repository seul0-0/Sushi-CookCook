using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoAttack : MonoBehaviour
{
    public int autoAttackLevel = 0;
    public Toggle autoAttackToggle; 
    public Button levelUpbutton;
    private Coroutine autoAttackRoutine;

    void Start()
    {
        // 토글 이벤트 연결
        autoAttackToggle.onValueChanged.AddListener(OnToggleChanged);
        levelUpbutton.onClick.AddListener(LevelUp);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn && autoAttackLevel > 0)
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

    public void LevelUp()
    {
        autoAttackLevel++;
        Debug.Log("AutoAttack Level: " + autoAttackLevel);

        // 토글 켜져 있으면 새 레벨에 맞게 다시 코루틴 실행
        if (autoAttackToggle.isOn)
        {
            OnToggleChanged(true);
        }
    }

    IEnumerator AutoAttackCoroutine() // 자동공격 실행 부분
    {
        while (true)
        {
            float attackInterval = Mathf.Max(0.5f, 2f - autoAttackLevel * 0.2f);
            yield return new WaitForSeconds(attackInterval);

            EventManager.attackClick?.Invoke();
            Debug.Log("자동공격 실행");
        }
    }
}
