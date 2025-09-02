using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingSpinner : MonoBehaviour
{
    public Image dotPrefab;      // �� �̹��� �ϳ� ������
    public int dotCount = 18;    // �� �� ����
    public float radius = 40f;   // ���� ������
    public float speed = 5f;     // ��� ��ȭ �ӵ�

    private Image[] dots;

    void Start()
    {
        dots = new Image[dotCount];

        for (int i = 0; i < dotCount; i++)
        {
            // �� ����
            Image dot = Instantiate(dotPrefab, transform);
            dots[i] = dot;

            // �������� ��ġ ��ġ
            float angle = i * Mathf.PI * 2f / dotCount;
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            dot.rectTransform.anchoredPosition = pos;
        }
    }

    void Update()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            float t = (Time.time * speed + i) % dots.Length / dots.Length;
            float intensity = Mathf.Sin(t * Mathf.PI * 2f) * 0.5f + 0.5f;
            dots[i].color = new Color(intensity, intensity, intensity, 1f);
        }
    }
}
