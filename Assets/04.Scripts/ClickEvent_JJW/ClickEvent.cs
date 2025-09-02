using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{
    public Button AttackButton;
    public Toggle autoAttackToggle;

    void Start()
    {
        AttackButton.onClick.AddListener(ClickButton);
        autoAttackToggle.onValueChanged.AddListener(delegate { AutoToggleEvent(); });
    }

    void ClickButton()
    {
        EventManager.attackClick?.Invoke();
    }
    void AutoToggleEvent()
    {
        EventManager.autoAttack?.Invoke();
    }

}
