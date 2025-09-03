using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public enum GameScene
{
    [SceneName("CJH_GameManager")] MainMenu,
    [SceneName("Test2")] Level1,
    [SceneName("Level2")] Level2,
    [SceneName("GameOver")] GameOver
}

[AttributeUsage(AttributeTargets.Field)]
public class SceneNameAttribute : Attribute
{
    public string Name { get; }
    public SceneNameAttribute(string name) => Name = name;
}

public static class SceneUtility
{
    public static string GetSceneName(GameScene scene)
    {
        var type = typeof(GameScene);
        var memInfo = type.GetMember(scene.ToString());
        var attr = memInfo[0].GetCustomAttributes(typeof(SceneNameAttribute), false);
        return (attr.Length > 0) ? ((SceneNameAttribute)attr[0]).Name : scene.ToString();
    }
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
        // Mediator 생성 (PlayerDataM을 IPlayerData로 전달)
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        mediator = new EventMediator(new PlayerDataM(statsData));

        _settingPanel.InitPanel();

        _audioManager.PlayBGM("1");

        // ESC 입력으로 UI 토글
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => _settingPanel.OnToggleSettings())
            .AddTo(this);
    }

    public void AddLevel(int value)
    {
        gameLevel += value;
    }

    //진행중인 스테이지 관리할 로직
}