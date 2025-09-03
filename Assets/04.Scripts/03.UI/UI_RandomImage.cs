using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RandomImage : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite[] sprites;          // �������� ����� ��������Ʈ �迭
    public Image targetImage;         // ��������Ʈ�� ǥ���� Image ������Ʈ

    void OnEnable()
    {
        AssignRandomSprite();
    }

    void AssignRandomSprite()
    {
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning("sprites �迭�� ����ֽ��ϴ�!");
            return;
        }

        if (targetImage == null)
        {
            Debug.LogWarning("targetImage�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // ���� ��������Ʈ �Ҵ�
        targetImage.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
