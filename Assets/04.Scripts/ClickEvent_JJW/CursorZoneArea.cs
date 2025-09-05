using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CursorZone : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    private bool isOver = false;

    void Update()
    {
        // 마우스가 UI 위에 있으면 강제로 기본 커서로 되돌리기
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            if (isOver)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isOver = false;
            }
            return; // UI 위라면 밑에 로직 실행 안 함
        }

        // UI 위가 아닐 때만 Collider 감지
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<Collider2D>().OverlapPoint(mousePos))
        {
            if (!isOver)
            {
                Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
                isOver = true;
            }
        }
        else
        {
            if (isOver)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isOver = false;
            }
        }
    }
}

