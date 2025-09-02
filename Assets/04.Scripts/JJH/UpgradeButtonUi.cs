using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeButtonUi : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === 델리 게이트 호출 ===

    public StatusUpgradePanel statusUpgradePanel;

    [Header("UpgradeBtn")]
    public TextMeshProUGUI nextCost;
    public Button upgradeBtn;

    // === 현재 버튼 번호 ===
    private int _buttonindex;
    // === 업그레이드 비용 ===
    private int _upgradeCost;
    // === 업그레이드 true일 경우 가능 ===
    private bool _isUpgradeReady;
    // === 클릭시 활성화 ===
    private bool _isClick;

    public void Start()
    {
        if(statusUpgradePanel != null && statusUpgradePanel.status != null)
        {
            _buttonindex = statusUpgradePanel.upgrade_id;

            SetButtonPanel();

            //upgradeBtn.onClick.AddListener(OnClick);

            PlayerStatus.OnMoneyChanged += CheckCost;
        }

    }

    // === 버튼에 할당 ===
    public void OnClick()
    {
        Debug.Log("클릭");

        _isClick = !_isClick;

        UpgradeStatus();
    }

    // === 다음 레벨 확인 ===
    public void SetButtonPanel()
    {
        _upgradeCost = statusUpgradePanel.status.CheckMoney(statusUpgradePanel.status.stats[_buttonindex].type);

        nextCost.text = $"{_upgradeCost}";

        CheckCost();
    }

    public void CheckCost()
    {
        // === 돈이 부족할 경우 ===
        if (statusUpgradePanel.status.money < _upgradeCost)
        {
            upgradeBtn.image.color = Color.red;
            _isUpgradeReady = false;
            return;
        }

        upgradeBtn.image.color = Color.black;

        _isUpgradeReady = true;
    }
   
    public void UpgradeStatus()
    {
        if (_isUpgradeReady == false) { return; }

        // === 업그레이드 ===
        statusUpgradePanel.status.UpgradeValue(statusUpgradePanel.status.stats[_buttonindex].type);

        statusUpgradePanel.status.ChangeMoneyValue(-_upgradeCost);

        // StartCoroutine(UpgradeCorutine());

        // === 업그레이드 체크를 다시 활성화 하기 위해 ===
        _isUpgradeReady = false;

        // === 패널 창 갱신 ===
        statusUpgradePanel.NextValue();

        // === 다음 비용 갱신 ===
        SetButtonPanel();

        // === 현재 스텟 창 갱신 ===
        OnStatusRefreshed?.Invoke();
    }

    public IEnumerator UpgradeCorutine()
    {
        yield return new WaitForSeconds(0.2f);

        while (_isClick && _isUpgradeReady)
        {
            // === 업그레이드 ===
            statusUpgradePanel.status.UpgradeValue(statusUpgradePanel.status.stats[_buttonindex].type);

            statusUpgradePanel.status.ChangeMoneyValue(-_upgradeCost);

            CheckCost();

            // === 패널 창 갱신 ===
            statusUpgradePanel.NextValue();

            // === 다음 비용 갱신 ===
            SetButtonPanel();

            // === 현재 스텟 창 갱신 ===
            OnStatusRefreshed?.Invoke();

            yield return new WaitForSeconds(0.2f);
        }

    }
}
