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
        // Mediator 생성 (PlayerDataM을 IPlayerData로 전달)
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        mediator = new EventMediator(new PlayerDataM(statsData));

        _audioManager.PlayBGM("1");
    }

    public void AddLevel(int value)
    {
        gameLevel += value;
    }

    //진행중인 스테이지 관리할 로직
}