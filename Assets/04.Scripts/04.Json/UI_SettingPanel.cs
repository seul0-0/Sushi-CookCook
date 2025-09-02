using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private GameObject _savePanel;
    [SerializeField] private GameObject _overPanel;

    [Inject] private IAudioManager _audioManager;


    public void InitPanel()
    {
        // 초기 슬라이더 값 세팅
        _bgmSlider.value = _audioManager.bgmVolume;
        _sfxSlider.value = _audioManager.sfxVolume;

        // 이벤트 등록
        _bgmSlider.onValueChanged.AddListener(value => _audioManager.SetBgmVolume(value));
        _sfxSlider.onValueChanged.AddListener(value => _audioManager.SetSfxVolume(value));

        _savePanel.SetActive(false);
        _overPanel.SetActive(false);
    }

    public void OpenUI()
    {
        Time.timeScale = 0f;
    }
    public async void CloseUI()
    {
        Time.timeScale = 1f;
    }

    public void OnToggleSettings()
    {
        if (!gameObject.activeSelf)
        {
            OpenUI();
            gameObject.SetActive(true);

            // 꺼졌다 켤 때는 슬라이더 값만 동기화
            _bgmSlider.value = _audioManager.bgmVolume;
            _sfxSlider.value = _audioManager.sfxVolume;
        }
        else
        {
            CloseUI();
            gameObject.SetActive(false);
        }
    }

    public void OnCloseTheScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OnSave()
    {
        _savePanel.SetActive(!_savePanel.activeSelf);
    }
    public void OnLoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // 현재 씬 인덱스
        SceneManager.LoadScene(currentSceneIndex + 1); // 다음 씬으로 이동
    }
    public void OnGodMode()
    {

    }

    public void OnBgmSlider(float value)
    {
        _audioManager.SetBgmVolume(value);
    }
    public void OnSfxSlider(float value)
    {
        _audioManager.SetSfxVolume(value);
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