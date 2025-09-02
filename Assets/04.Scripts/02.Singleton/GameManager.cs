using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public interface IGameManager
{
    void AddLevel(int value);
}

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] private UI_SettingPanel _settingPanel;

    [Inject] private IAudioManager _audioManager;

    public int gameLevel {  get;  set; }

    public EventMediator mediator;

    private void Awake()
    {
        _settingPanel ??= GetComponentInChildren<UI_SettingPanel>(); 
    }

    async void Start()
    {
        // Mediator ���� (PlayerDataM�� IPlayerData�� ����)
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        mediator = new EventMediator(new PlayerDataM(statsData));

        _audioManager.PlayBGM("1");
    }

    public void AddLevel(int value)
    {
        gameLevel += value;
    }

    //�������� �������� ������ ����
}