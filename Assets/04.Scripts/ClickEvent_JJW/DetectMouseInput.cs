using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D cursorTexture; // 바꿀 커서 이미지
    public Vector2 hotSpot = Vector2.zero; // 커서 클릭 포인트 기준 (이미지의 중심 좌표 같은 것)

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // 기본 커서로 복원
    }
}