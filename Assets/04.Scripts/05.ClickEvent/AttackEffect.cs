using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour
{
    public GameObject swordEffectPrefab; // UI용 칼 이펙트 프리팹

    void Start()
    {
        EventManager.attackClick += SpawnEffectAtCursor;
    }
    public void SpawnEffectAtCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        GameObject effect = Instantiate(swordEffectPrefab, mousePosition, Quaternion.identity);

        Destroy(effect, 0.3f); // 0.5초 뒤 파괴
    }
}
