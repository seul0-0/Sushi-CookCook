using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // UI 위 클릭이면 무시
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HandleClick();
            }
            else
            {
                return;
            }

        }
    }

    void HandleClick()
    {
        EventManager.attackClick?.Invoke();
    }
}
