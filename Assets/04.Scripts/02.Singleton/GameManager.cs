using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public enum GameScene
{
    [SceneName("IntroScene")] MainMenu,
    [SceneName("MainScene")] Level1,
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
    EventMediator mediator { get; }
}

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] private UI_SettingPanel _settingPanel;
    [Inject] private IAudioManager _audioManager;

    public int gameLevel { get; private set; }
    public EventMediator mediator { get; private set; }


    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        _settingPanel ??= GetComponentInChildren<UI_SettingPanel>(); 
    }

    void Start()
    {
        // PlayerStatsTest + 무기 DB 로드
        List<WeaponDataSO> weaponDB = Resources.LoadAll<WeaponDataSO>("Weapons").ToList();
        // mediator 생성

        PlayerDataM playerData = new PlayerDataM(); // 먼저 변수 선언
        mediator = new EventMediator(playerData);           // 변수 넣기


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