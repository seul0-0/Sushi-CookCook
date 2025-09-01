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

    private bool _isClick = false;                 // === 클릭하지 않을경우 ===

    private void Start()
    {
        statusbtn.onClick.AddListener(ClickButton);

        MakeWindow();

        upgradeWindow.SetActive(false);
    }

    private void ClickButton()
    {
        _isClick = !_isClick;                   // === 클릭시 창이 나오고 창이 있을경우 false로 닫음 ===

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
