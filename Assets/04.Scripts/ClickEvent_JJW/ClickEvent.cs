using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{
    public Image HealthGauge;
    public Button AttackButton;
    
    void Start()
    {
        AttackButton.onClick.AddListener(Test);
        EventManager.attackClick?.Invoke();
    }

    void Test()
    {
        float reduceHp = -0.2f;
        Debug.Log("버튼 클릭");
        HealthGauge.fillAmount += reduceHp; 
    }

}
