using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{
    void OnMouseDown()   // 2D Collider가 있는 오브젝트 클릭 시 실행
    {
        if (Input.GetMouseButtonDown(0))
        {
            // UI 위 클릭이면 무시
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // 여기부터는 실제 게임 월드 클릭 처리
            HandleClick();
        }
        void HandleClick()
        {
            Debug.Log("게임 오브젝트 클릭 처리");
            // → 공격 이벤트 호출
            EventManager.attackClick?.Invoke();
        }
    }
}
