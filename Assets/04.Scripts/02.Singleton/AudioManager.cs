using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[System.Serializable]
public class NamedBGM
{
    public string name;
    public AudioClip clip;
}
[System.Serializable]
public class NamedSFX
{
    public string name;
    public AudioClip clip;
}
public interface IAudioManager
{
    float bgmVolume { get; }
    float sfxVolume { get; }

    public void SetBgmVolume(float volume);
    public void SetSfxVolume(float volume);
    void PlayBGM(string name);
    void PlaySFX(string name);
    UniTask FadeToBGM(string name, float duration);
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer; // MainMixer
    [SerializeField] private AudioMixerGroup bgmGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;

    [Header("BGM Clips")]
    [SerializeField] private List<NamedBGM> bgmList;

    [Header("SFX Clips")]
    [SerializeField] private List<NamedSFX> sfxList;
    public int sfxPoolSize = 5; //동시재생가능한수

    private List<AudioSource> sfxSources = new List<AudioSource>();
    private int currentSfxIndex = 0;
    private AudioSource bgmSource;

    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    [Range(0f, 1f)]
    [SerializeField] private float _bgmVolume = 0.25f;
    public float bgmVolume => _bgmVolume;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 0.5f;
    public float sfxVolume => _bgmVolume;


    private void Awake()
    {
        // BGM 오디오 소스 생성
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = bgmGroup;
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        // SFX 풀 초기화
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfx = gameObject.AddComponent<AudioSource>();
            sfx.playOnAwake = false;
            sfx.outputAudioMixerGroup = sfxGroup;
            sfxSources.Add(sfx);
        }

        // 딕셔너리 초기화
        foreach (var bgm in bgmList)
        {
            if (!bgmDict.ContainsKey(bgm.name))
                bgmDict.Add(bgm.name, bgm.clip);
        }

        foreach (var sfx in sfxList)
        {
            if (!sfxDict.ContainsKey(sfx.name))
                sfxDict.Add(sfx.name, sfx.clip);
        }

        SetBgmVolume(_bgmVolume);
        SetSfxVolume(_sfxVolume);
    }

    void Update()
    {
        // 항상 볼륨 최신화 (옵션에서 슬라이더로 조절 시 반영되게)
        bgmSource.volume = bgmVolume;
        foreach (var sfx in sfxSources) sfx.volume = _sfxVolume;

        // 버튼 클릭 SFX
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject;
            if (clicked != null && clicked.GetComponent<Button>() != null)
                PlayClickSFX();
        }
    }

    #region Mixer Volume Control
    private float LinearToDecibel(float linear) => linear <= 0f ? -80f : Mathf.Log10(linear) * 20f;

    public void SetBgmVolume(float volume)
    {
        _bgmVolume = Mathf.Clamp01(volume);
        audioMixer.SetFloat("BGM_Volume", LinearToDecibel(_bgmVolume));
    }

    public void SetSfxVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        audioMixer.SetFloat("SFX_Volume", LinearToDecibel(_sfxVolume));
    }
    #endregion


    #region BGM
    public void PlayBGM(string name)
    {
        if (!bgmDict.TryGetValue(name, out var clip)) return;
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public async UniTask FadeToBGM(string name, float duration)
    {
        if (!bgmDict.TryGetValue(name, out var newClip)) return;

        float startVolume = _bgmVolume;
        float time = 0f;

        // 페이드 아웃
        while (time < duration)
        {
            time += Time.deltaTime;
            SetBgmVolume(Mathf.Lerp(startVolume, 0f, time / duration));
            await UniTask.Yield();
        }

        // 새 클립 재생
        bgmSource.clip = newClip;
        bgmSource.Play();

        // 페이드 인
        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            SetBgmVolume(Mathf.Lerp(0f, startVolume, time / duration));
            await UniTask.Yield();
        }
    }
    #endregion


    #region SFX
    public void PlaySFX(string name)
    {
        if (!sfxDict.TryGetValue(name, out var clip)) return;

        sfxSources[currentSfxIndex].PlayOneShot(clip);
        currentSfxIndex = (currentSfxIndex + 1) % sfxPoolSize;
    }

    public void PlayClickSFX() => PlaySFX("Click");
    #endregion
}