using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimationController : MonoBehaviour
{
    private Animator animator;
    private EnemyStatusTest enemyStatusTest;
    EnemyUI enemyUI;
    private float previousHpRatio;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyUI = FindObjectOfType<EnemyUI>();
        previousHpRatio = enemyUI.hpBar.fillAmount;
    }

    void Update()
    {
        float currentHpRatio = enemyUI.hpBar.fillAmount;
        if (currentHpRatio < previousHpRatio && currentHpRatio > 0)
        {
            PlayHit();
        }

        if (currentHpRatio <= 0)
        {
            PlayDie();
        }

        previousHpRatio = currentHpRatio; // 다음 프레임 비교용
    }
    public void PlayHit()
    {
        animator.SetTrigger("isHit");
    }
    public void PlayDie()
    {
        animator.SetTrigger("isDie");
    }
}
