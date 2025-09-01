using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Button statusbtn;
    public Transform content;

    public GameObject upgradeWindow;
    public GameObject upgradeWindowPrefabs;

    private bool _isClick = false;                 // === Ŭ������ ������� ===

    private void Start()
    {
        statusbtn.onClick.AddListener(ClickButton);

        MakeWindow();

        upgradeWindow.SetActive(false);
    }

    private void ClickButton()
    {
        _isClick = !_isClick;                   // === Ŭ���� â�� ������ â�� ������� false�� ���� ===

        upgradeWindow.SetActive(_isClick);
    }

    private void MakeWindow()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject newWindow = Instantiate(upgradeWindowPrefabs);

            newWindow.transform.SetParent(content);
        }
    }

}
