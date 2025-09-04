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
    public PlayerDataM playerData;
    public List<WeaponDataSO> weaponDB;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        _settingPanel ??= GetComponentInChildren<UI_SettingPanel>(); 
    }

    void Start()
    {
        // PlayerStatsTest 로드
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        // EventMediator 생성
        // Save 불러오기
        var saveData = SaveManager.LoadPlayer();
        if (saveData != null)
        {
            playerData.LoadFromSave(saveData, weaponDB);
        }

        mediator = new EventMediator(playerData);


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

    public void SaveGame()
    {
        SaveManager.SavePlayer(playerData);
        Debug.Log("게임 저장 완료!");
    }

    public void LoadGame()
    {
        var saveData = SaveManager.LoadPlayer();
        if (saveData != null)
        {
            playerData.LoadFromSave(saveData, weaponDB);
            Debug.Log("게임 로드 완료!");
        }
        else
        {
            Debug.Log("저장 데이터 없음");
        }
    }
}