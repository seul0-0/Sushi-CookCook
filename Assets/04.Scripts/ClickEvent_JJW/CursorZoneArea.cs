using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorZone : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    private bool isOver = false;

    void Update()
    {
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

