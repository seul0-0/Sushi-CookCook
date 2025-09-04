using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    public EnemyStatusTest enemyStatus;
    public Image hpBar;

    void Update()
    {
        hpBar.fillAmount = enemyStatus.GetHpRatio();
    }
}
