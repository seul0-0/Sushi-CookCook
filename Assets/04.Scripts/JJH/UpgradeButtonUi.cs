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
    private bool _isClickHold;
    // === 코루틴 ===
    private Coroutine _coroutine;

    public void Start()
    {
        if(statusUpgradePanel != null && StatusManager.Instance.status != null)
        {
            _buttonindex = statusUpgradePanel.upgrade_id;

            SetButtonPanel();

            upgradeBtn.onClick.AddListener(OnClick);

            // === 돈 변화 감지후 ui 갱신 ===
            PlayerStatus.OnMoneyChanged += CheckCost;
        }

    }

    // === 버튼을 누름 ===
    public void OnClick()
    {
        _coroutine = StartCoroutine(UpgradeCorutine());

        _isClickHold = true;
    }

    // === 버튼을 누르지 않음 ===
    public void CancellClick()
    {
        _isClickHold = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }


    // === 다음 레벨 확인 ===
    public void SetButtonPanel()
    {
        _upgradeCost = StatusManager.Instance.status.CheckMoney(StatusManager.Instance.status.stats[_buttonindex].type);

        nextCost.text = $"{_upgradeCost}";

        CheckCost();
    }

    public void CheckCost()
    {
        // === 돈이 부족할 경우 ===
        if (StatusManager.Instance.status.money < _upgradeCost)
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
        StatusManager.Instance.status.UpgradeValue(StatusManager.Instance.status.stats[_buttonindex].type);

        StatusManager.Instance.status.ChangeMoneyValue(-_upgradeCost);

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
        if (_isUpgradeReady == false) 
        {
            _isClickHold = false;
            yield break; 
        }

        UpgradeStatus();

        yield return new WaitForSeconds(0.2f);

        if (_isClickHold == true)
        {
            while (true) 
            {
                UpgradeStatus();

                yield return new WaitForSeconds(0.2f);
            }

        }

    }
}
