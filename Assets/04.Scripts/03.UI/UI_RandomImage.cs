using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RandomImage : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite[] sprites;          // 랜덤으로 사용할 스프라이트 배열
    public Image targetImage;         // 스프라이트를 표시할 Image 컴포넌트

    void OnEnable()
    {
        AssignRandomSprite();
    }

    void AssignRandomSprite()
    {
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning("sprites 배열이 비어있습니다!");
            return;
        }

        if (targetImage == null)
        {
            Debug.LogWarning("targetImage가 할당되지 않았습니다!");
            return;
        }

        // 랜덤 스프라이트 할당
        targetImage.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
