using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingSpinner : MonoBehaviour
{
    public Image dotPrefab;      // 원 이미지 하나 프리팹
    public int dotCount = 18;    // 총 원 개수
    public float radius = 40f;   // 원형 반지름
    public float speed = 5f;     // 밝기 변화 속도

    private Image[] dots;

    void Start()
    {
        dots = new Image[dotCount];

        for (int i = 0; i < dotCount; i++)
        {
            // 원 복제
            Image dot = Instantiate(dotPrefab, transform);
            dots[i] = dot;

            // 원형으로 위치 배치
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
