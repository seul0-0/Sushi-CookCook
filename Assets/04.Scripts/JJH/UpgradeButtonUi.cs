using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUi : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === ���� ����Ʈ ȣ�� ===

    public StatusUpgradePanel statusUpgradePanel;

    [Header("UpgradeBtn")]
    public TextMeshProUGUI nextCost;
    public Button upgradeBtn;

    // === ��ư �ִϸ��̼� ===
    private Animator _anim;
    // === ���� ��ư ��ȣ ===
    private int _buttonindex;
    // === ���׷��̵� ��� ===
    private int _upgradeCost;
    // === ���׷��̵� true�� ��� ���� ===
    private bool _isUpgradeReady;
    // === Ŭ���� Ȱ��ȭ ===
    private bool _isClickHold;
    // === �ڷ�ƾ ===
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

            // === �� ��ȭ ������ ui ���� ===
            StatusManager.Instance.OnMoneyChanged += CheckButtonUi;
        }
    }

    // === ��ư�� ���� ===
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
            // === �ڵ����� �ִ� ���� ===
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

        // === 2�ʰ� ������ �ڵ� ���� ===
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

        // === ���׷��̵� ===
        StatusManager.Instance.UpgradeValue(StatusManager.Instance.currentStatus.stats[_buttonindex].type);

        StatusManager.Instance.ChangeMoneyValue(-_upgradeCost);

        // === ���׷��̵� üũ�� �ٽ� Ȱ��ȭ �ϱ� ���� ===
        _isUpgradeReady = false;

        // === �г� â ���� ===
        statusUpgradePanel.NextValue();

        // === ���� ��� ���� ===
        SetButtonPanel();

        // === ���� ���� â ���� ===
        OnStatusRefreshed?.Invoke();
    }

    // === ���� ���� Ȯ�� ===
    public void SetButtonPanel()
    {
        _upgradeCost = StatusManager.Instance.CheckMoney(StatusManager.Instance.currentStatus.stats[_buttonindex].type);

        nextCost.text = $"{_upgradeCost}";

        CheckButtonUi();
    }

    // === ��ư �� ���� ===
    public void CheckButtonUi()
    {
        // === ���� ������ ��� ===
        if (StatusManager.Instance.currentStatus.money < _upgradeCost)
        {
            upgradeBtn.image.color = Color.red;

            _isUpgradeReady = false;
            // === �ڵ����� ���� ===
            _anim.SetBool("isComplete", false);
            return;
        }

        upgradeBtn.image.color = Color.black;

        _isUpgradeReady = true;
    }
}
