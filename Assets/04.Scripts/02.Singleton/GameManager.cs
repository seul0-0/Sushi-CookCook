using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public enum GameScene
{
    MainMenu,
    Level1,
    Level2,
    GameOver
}

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
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        _settingPanel ??= GetComponentInChildren<UI_SettingPanel>(); 
    }

    void Start()
    {
        // Mediator ���� (PlayerDataM�� IPlayerData�� ����)
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        mediator = new EventMediator(new PlayerDataM(statsData));

        _settingPanel.InitPanel();

        _audioManager.PlayBGM("1");

        // ESC �Է����� UI ���
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => _settingPanel.OnToggleSettings())
            .AddTo(this);
    }

    public void AddLevel(int value)
    {
        gameLevel += value;
    }

    //�������� �������� ������ ����
}