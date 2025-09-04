using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{
    void OnMouseDown()   // 2D Collider가 있는 오브젝트 클릭 시 실행
    {
        // UI 위 클릭은 무시 (옵션: EventSystem 체크)
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        EventManager.attackClick?.Invoke();
    }
}
