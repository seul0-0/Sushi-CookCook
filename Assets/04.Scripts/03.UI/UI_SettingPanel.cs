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
        // 초기 슬라이더 값 세팅
        _bgmSlider.value = _audioManager.bgmVolume;
        _sfxSlider.value = _audioManager.sfxVolume;

        // 슬라이더 이벤트 구독
        _bgmSlider.OnValueChangedAsObservable()
                  .Subscribe(value => _audioManager.SetBgmVolume(value)).AddTo(this);

        _sfxSlider.OnValueChangedAsObservable()
                  .Subscribe(value => _audioManager.SetSfxVolume(value)).AddTo(this);

        // 버튼 이벤트 구독
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


        // 패널 초기 상태
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
        // 5초 대기
        await UniTask.Delay(TimeSpan.FromSeconds(5));

        // 로딩판넬 비활성화
        _loadingPanel.SetActive(false);

        // 블러 + 페이드 씬 전환 실행
        await _blurPanel.LoadSceneWithBlurFade(scene);
    }


    public void OnToggleSettings()
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;

        if (isActive)
        {
            // 켰을 때 슬라이더 값 동기화
            _bgmSlider.value = _audioManager.bgmVolume;
            _sfxSlider.value = _audioManager.sfxVolume;
        }
    }

    public void OnSavePanelOpen()
    {
        _savePanel.SetActive(!_savePanel.activeSelf);

        if (_savePanel.activeSelf)
        {
            // Save Panel 열릴 때 버튼 이벤트 등록
            _saveAcceptButton.onClick.RemoveAllListeners();
            _saveAcceptButton.onClick.AddListener(() =>
            {
                SaveManager.Save();
                Debug.Log("저장 완료");
                _savePanel.SetActive(false);
            });

            _saveDeclineButton.onClick.RemoveAllListeners();
            _saveDeclineButton.onClick.AddListener(() =>
            {
                // 나중에 취소 로직 연결
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
                Debug.Log("로드 완료");
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // 현재 씬 인덱스
        SceneManager.LoadScene(currentSceneIndex + 1); // 다음 씬으로 이동
    }
    public void OnGodMode()
    {

    }

    public void OnGameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 플레이 모드 종료
#else
              Application.Quit(); // 빌드된 게임 종료
#endif
    }
}