using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeButtonUi : MonoBehaviour
{
    public static Action OnStatusRefreshed;            // === ���� ����Ʈ ȣ�� ===

    public StatusUpgradePanel statusUpgradePanel;

    [Header("UpgradeBtn")]
    public TextMeshProUGUI nextCost;
    public Button upgradeBtn;

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

    public void Start()
    {
        if(statusUpgradePanel != null && StatusManager.Instance.status != null)
        {
            _buttonindex = statusUpgradePanel.upgrade_id;

            SetButtonPanel();

            upgradeBtn.onClick.AddListener(OnClick);

            // === �� ��ȭ ������ ui ���� ===
            PlayerStatus.OnMoneyChanged += CheckCost;
        }

    }

    // === ��ư�� ���� ===
    public void OnClick()
    {
        _coroutine = StartCoroutine(UpgradeCorutine());

        _isClickHold = true;
    }

    // === ��ư�� ������ ���� ===
    public void CancellClick()
    {
        _isClickHold = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }


    // === ���� ���� Ȯ�� ===
    public void SetButtonPanel()
    {
        _upgradeCost = StatusManager.Instance.status.CheckMoney(StatusManager.Instance.status.stats[_buttonindex].type);

        nextCost.text = $"{_upgradeCost}";

        CheckCost();
    }

    public void CheckCost()
    {
        // === ���� ������ ��� ===
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

        // === ���׷��̵� ===
        StatusManager.Instance.status.UpgradeValue(StatusManager.Instance.status.stats[_buttonindex].type);

        StatusManager.Instance.status.ChangeMoneyValue(-_upgradeCost);

        // === ���׷��̵� üũ�� �ٽ� Ȱ��ȭ �ϱ� ���� ===
        _isUpgradeReady = false;

        // === �г� â ���� ===
        statusUpgradePanel.NextValue();

        // === ���� ��� ���� ===
        SetButtonPanel();

        // === ���� ���� â ���� ===
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
