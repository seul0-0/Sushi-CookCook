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
        // PlayerStatsTest �ε�
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        // EventMediator ����
        // Save �ҷ�����
        var saveData = SaveManager.LoadPlayer();
        if (saveData != null)
        {
            playerData.LoadFromSave(saveData, weaponDB);
        }

        mediator = new EventMediator(playerData);


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

    public void SaveGame()
    {
        SaveManager.SavePlayer(playerData);
        Debug.Log("���� ���� �Ϸ�!");
    }

    public void LoadGame()
    {
        var saveData = SaveManager.LoadPlayer();
        if (saveData != null)
        {
            playerData.LoadFromSave(saveData, weaponDB);
            Debug.Log("���� �ε� �Ϸ�!");
        }
        else
        {
            Debug.Log("���� ������ ����");
        }
    }
}