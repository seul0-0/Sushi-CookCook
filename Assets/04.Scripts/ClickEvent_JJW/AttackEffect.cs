using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour
{
    public Button attackButton;
    public GameObject swordEffectUIPrefab; // UI용 칼 이펙트 프리팹
    public Canvas canvas; // UI가 속한 캔버스

    void Start()
    {
        attackButton.onClick.AddListener(SpawnEffectAtCursor);
    }

    void SpawnEffectAtCursor()
    {
        // 마우스 위치 → UI 좌표 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 localPos
        );

        // 프리팹 생성 (Canvas 안에 자식으로)
        GameObject effect = Instantiate(swordEffectUIPrefab, canvas.transform);
        effect.GetComponent<RectTransform>().anchoredPosition = localPos;

        // 1초 뒤 파괴
        Destroy(effect, 0.5f);
    }
}
