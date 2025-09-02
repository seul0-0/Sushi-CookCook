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
        // �ʱ� �����̴� �� ����
        _bgmSlider.value = _audioManager.bgmVolume;
        _sfxSlider.value = _audioManager.sfxVolume;

        // �̺�Ʈ ���
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

            // ������ �� ���� �����̴� ���� ����ȭ
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ���� �� �ε���
        SceneManager.LoadScene(currentSceneIndex + 1); // ���� ������ �̵�
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
        UnityEditor.EditorApplication.isPlaying = false; // ������ �÷��� ��� ����
#else
              Application.Quit(); // ����� ���� ����
#endif
    }
}