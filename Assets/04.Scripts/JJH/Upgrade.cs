using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Transform content;
    [Header("upgrade")]
    public GameObject upgradeWindowPrefabs;

    private void Start()
    {
        MakeWindow();
    }

    private void MakeWindow()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject newWindow = Instantiate(upgradeWindowPrefabs);

            newWindow.transform.SetParent(content);

            StatusUpgradePanel panel = newWindow.GetComponent<StatusUpgradePanel>();

            panel.SetPanel(i);
        }
    }

}
