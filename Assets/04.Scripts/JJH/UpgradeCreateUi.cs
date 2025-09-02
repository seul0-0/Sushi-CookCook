using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCreateUi : MonoBehaviour
{
    public Transform content;                 // === 생성 위치 ===

    [Header("upgrade")]
    public GameObject upgradeWindowPrefabs;

    private void Start()
    {
        MakeWindow();
    }

    private void MakeWindow()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newWindow = Instantiate(upgradeWindowPrefabs);

            newWindow.transform.SetParent(content);

            StatusUpgradePanel panel = newWindow.GetComponent<StatusUpgradePanel>();

            panel.SetPanel(i);
        }
    }
}
