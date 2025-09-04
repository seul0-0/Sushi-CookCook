using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Zenject;
using Scene = UnityEngine.SceneManagement.Scene;

public class UI_SettingPanel : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Panels")]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _savePanel;
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private UI_BlurFade _blurPanel;
    [SerializeField] private GameObject _loadingPanel;

    [Header("Buttons")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _saveAcceptButton;
    [SerializeField] private Button _saveDeclineButton;
    [SerializeField] private Button _overAcceptButton;
    [SerializeField] private Button _overDeclineButton;

    [Inject] private IAudioManager _audioManager;
    [Inject] private IGameManager _gameManager;


    public void InitPanel()
    {
        // �ʱ� �����̴� �� ����
        _bgmSlider.value = _audioManager.bgmVolume;
        _sfxSlider.value = _audioManager.sfxVolume;

        // �����̴� �̺�Ʈ ����
        _bgmSlider.OnValueChangedAsObservable()
                  .Subscribe(value => _audioManager.SetBgmVolume(value)).AddTo(this);

        _sfxSlider.OnValueChangedAsObservable()
                  .Subscribe(value => _audioManager.SetSfxVolume(value)).AddTo(this);

        // ��ư �̺�Ʈ ����
        _resumeButton.OnClickAsObservable()
                     .Subscribe(_ => OnToggleSettings())
                     .AddTo(this);

        _saveButton.OnClickAsObservable()
                   .Subscribe(_ => OnSavePanelOpen())
                   .AddTo(this);

        _loadButton.OnClickAsObservable()
                   .Subscribe(_ => OnLoadPanelOpen())
                   .AddTo(this);

        _quitButton.OnClickAsObservable()
                   .Subscribe(_ => OnCloseTheScene())
                   .AddTo(this);

        _startButton.OnClickAsObservable()
           .Subscribe(_ => OnGameStart())
           .AddTo(this);


        // �г� �ʱ� ����
        _startPanel.SetActive(true);
        _savePanel.SetActive(false);
        _loadPanel.SetActive(false);
        gameObject.SetActive(false);
        _blurPanel.gameObject.SetActive(false);
        _loadingPanel.SetActive(false);
    }

    public void OnGameStart()
    {
        _startPanel.SetActive(false);
        DelayAndLoad(GameScene.Level1).Forget();
        _audioManager.SetBgmVolume(0.5f);
        _audioManager.PlayBGM("2");
    }
    private async UniTask DelayAndLoad(GameScene scene)
    {
        _loadingPanel.SetActive(true);
        // 5�� ���
        await UniTask.Delay(TimeSpan.FromSeconds(5));

        // �ε��ǳ� ��Ȱ��ȭ
        _loadingPanel.SetActive(false);

        // �� + ���̵� �� ��ȯ ����
        await _blurPanel.LoadSceneWithBlurFade(scene);
    }


    public void OnToggleSettings()
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;

        if (isActive)
        {
            // ���� �� �����̴� �� ����ȭ
            _bgmSlider.value = _audioManager.bgmVolume;
            _sfxSlider.value = _audioManager.sfxVolume;
        }
    }

    public void OnSavePanelOpen()
    {
        _savePanel.SetActive(!_savePanel.activeSelf);

        if (_savePanel.activeSelf)
        {
            // Save Panel ���� �� ��ư �̺�Ʈ ���
            _saveAcceptButton.onClick.RemoveAllListeners();
            _saveAcceptButton.onClick.AddListener(() =>
            {
                SaveManager.Save();
                Debug.Log("���� �Ϸ�");
                _savePanel.SetActive(false);
            });

            _saveDeclineButton.onClick.RemoveAllListeners();
            _saveDeclineButton.onClick.AddListener(() =>
            {
                // ���߿� ��� ���� ����
                _savePanel.SetActive(false);
            });
        }
    }

    public void OnLoadPanelOpen()
    {
        _loadPanel.SetActive(!_loadPanel.activeSelf);

        if (_loadPanel.activeSelf)
        {
            _overAcceptButton.onClick.RemoveAllListeners();
            _overAcceptButton.onClick.AddListener(() =>
            {
                Debug.Log("�ε� �Ϸ�");
                SaveManager.Load();
                _loadPanel.SetActive(false);
            });

            _overDeclineButton.onClick.RemoveAllListeners();
            _overDeclineButton.onClick.AddListener(() =>
            {
                _loadPanel.SetActive(false);
            });
        }
    }


    public void OnCloseTheScene()
    {
        SceneManager.LoadScene(SceneUtility.GetSceneName(GameScene.MainMenu));
        _startPanel.SetActive(true);
        OnToggleSettings();
    }


    public void OnLoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ���� �� �ε���
        SceneManager.LoadScene(currentSceneIndex + 1); // ���� ������ �̵�
    }
    public void OnGodMode()
    {

    }

    public void OnGameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ �÷��� ��� ����
#else
              Application.Quit(); // ����� ���� ����
#endif
    }
}