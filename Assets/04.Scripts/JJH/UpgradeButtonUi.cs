using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUi : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === 델리 게이트 호출 ===

    public StatusUpgradePanel statusUpgradePanel;

    [Header("UpgradeBtn")]
    public TextMeshProUGUI nextCost;
    public Button upgradeBtn;

    // === 버튼 애니메이션 ===
    private Animator _anim;
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

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Start()
    {
        if(statusUpgradePanel != null && StatusManager.Instance.currentStatus != null)
        {
            _buttonindex = statusUpgradePanel.upgrade_id;

            SetButtonPanel();

            upgradeBtn.onClick.AddListener(OnClick);

            // === 돈 변화 감지후 ui 갱신 ===
            StatusManager.Instance.OnMoneyChanged += CheckButtonUi;
        }
    }

    // === 버튼을 누름 ===
    public void OnClick()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        else
        {
            _isClickHold = !_isClickHold;
            // === 자동구매 애니 멈춤 ===
            _anim.SetBool("isComplete", false);

            _coroutine = StartCoroutine(UpgradeCorutine());
        }
    }

    public IEnumerator UpgradeCorutine()
    {
        if (_isUpgradeReady == false)
        {
            _isClickHold = false;

            yield break;
        }

        UpgradeStatus();

        // === 2초간 누를시 자동 업글 ===
        yield return new WaitForSeconds(2f);

        if (_isClickHold == false)
            yield break;

        while (_isClickHold)
        {
            UpgradeStatus();

            _anim.SetBool("isComplete", true); 

            yield return new WaitForSeconds(0.2f);
        }
    }
    public void UpgradeStatus()
    {
        if (_isUpgradeReady == false) { return; }

        // === 업그레이드 ===
        StatusManager.Instance.UpgradeValue(StatusManager.Instance.currentStatus.stats[_buttonindex].type);

        StatusManager.Instance.ChangeMoneyValue(-_upgradeCost);

        // === 업그레이드 체크를 다시 활성화 하기 위해 ===
        _isUpgradeReady = false;

        // === 패널 창 갱신 ===
        statusUpgradePanel.NextValue();

        // === 다음 비용 갱신 ===
        SetButtonPanel();

        // === 현재 스텟 창 갱신 ===
        OnStatusRefreshed?.Invoke();
    }

    // === 다음 레벨 확인 ===
    public void SetButtonPanel()
    {
        _upgradeCost = StatusManager.Instance.CheckMoney(StatusManager.Instance.currentStatus.stats[_buttonindex].type);

        nextCost.text = $"{_upgradeCost}";

        CheckButtonUi();
    }

    // === 버튼 색 갱신 ===
    public void CheckButtonUi()
    {
        // === 돈이 부족할 경우 ===
        if (StatusManager.Instance.currentStatus.money < _upgradeCost)
        {
            upgradeBtn.image.color = Color.red;

            _isUpgradeReady = false;
            // === 자동구매 멈춤 ===
            _anim.SetBool("isComplete", false);
            return;
        }

        upgradeBtn.image.color = Color.black;

        _isUpgradeReady = true;
    }
}
