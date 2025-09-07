using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CursorZone : MonoBehaviour
{
    private Texture2D currentCursor;
    private Vector2 hotSpot;
    private bool isOver = false;

    void OnEnable()
    {
        if (EquipManager.Instance != null)
        {
            EquipManager.OnCursorChanged += UpdateCursor;

            // 초기 커서 세팅 (기본 장착 아이템)
            if (EquipManager.Instance.currentWeapon.Count > 0)
            {
                var firstItem = EquipManager.Instance.currentWeapon[0];
                if (firstItem != null && firstItem.CursorTexture != null)
                {
                    UpdateCursor(firstItem.CursorTexture, firstItem.CursorHotspot);
                }
            }
        }
    }

    void OnDisable() // 아이템 장착 해제
    {
        if (EquipManager.Instance != null)
            EquipManager.OnCursorChanged -= UpdateCursor;
    }

    void UpdateCursor(Texture2D texture, Vector2 hotspot) // 장비별로 존재하는 커서 이미지 업데이트
    {
        currentCursor = texture;
        hotSpot = hotspot;

        // Collider 위에 있을 때 바로 적용
        if (isOver)
            Cursor.SetCursor(currentCursor, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool overCollider = GetComponent<Collider2D>().OverlapPoint(mousePos);

        if (overUI)
        {
            if (isOver)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isOver = false;
            }
            return;
        }

        if (overCollider)
        {
            if (!isOver)
            {
                Cursor.SetCursor(currentCursor, hotSpot, CursorMode.Auto);
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