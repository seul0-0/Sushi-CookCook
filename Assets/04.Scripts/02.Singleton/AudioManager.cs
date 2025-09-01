using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IAudioManager
{
}

[System.Serializable]
public class NamedSFX
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    [Header("BGM Clips")]
    public AudioClip bgmStart;
    public AudioClip bgmGame;
    public AudioClip bgmGame2;
    public AudioClip bgmGame3;
    public AudioSource bgmSource;

    [Header("SFX Clips")]
    public List<NamedSFX> sfxClips;
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    public int sfxPoolSize = 5;

    private List<AudioSource> sfxSources = new List<AudioSource>();
    private int currentSfxIndex = 0;

    [Range(0f, 1f)] public float bgmVolume = 0.25f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // BGM 오디오 소스 생성
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        // SFX 풀 초기화
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfx = gameObject.AddComponent<AudioSource>();
            sfx.playOnAwake = false;
            sfxSources.Add(sfx);
        }

        // 사운드 이름-클립 매핑
        foreach (var sfx in sfxClips)
        {
            if (!sfxDict.ContainsKey(sfx.name))
                sfxDict.Add(sfx.name, sfx.clip);
            else
                Debug.LogWarning($"[AudioManager] 중복된 SFX 이름: {sfx.name}");
        }
    }

    void Update()
    {
        // 항상 볼륨 최신화 (옵션에서 슬라이더로 조절 시 반영되게)
        bgmSource.volume = bgmVolume;
        foreach (var sfx in sfxSources)
        {
            sfx.volume = sfxVolume;
        }
        //전역 버튼소리 활성화
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject;

            if (clicked != null && clicked.GetComponent<Button>() != null)
            {
                //AudioManager.Instance.PlayClickSFX();
            }
        }
    }
    public void PlayBGM(string name)
    {
        AudioClip clipToPlay = null;
        switch (name)
        {
            case "Start": clipToPlay = bgmStart; break;
            case "Game": clipToPlay = bgmGame; break;
            case "Game2": clipToPlay = bgmGame2; break;
            default: Debug.LogWarning("Unknown BGM name: " + name); return;
        }
        PlayBGM(clipToPlay);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSources[currentSfxIndex].PlayOneShot(clip);
        currentSfxIndex = (currentSfxIndex + 1) % sfxPoolSize;
    }
    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
        {
            AudioClip clip = sfxDict[name];
            sfxSources[currentSfxIndex].PlayOneShot(clip);
            currentSfxIndex = (currentSfxIndex + 1) % sfxPoolSize;
        }
        else
        {
            Debug.LogWarning($"[AudioManager] SFX '{name}' not found!");
        }
    }
    public void PlayClickSFX()
    {
        PlaySFX("Click"); // 또는 클릭 사운드 이름에 맞게 수정
    }


    // 외부에서 조절용
    public void SetBgmVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    IEnumerator NextBGM(AudioClip newClip, float duration)
    { //StartCoroutine(FadeBGM(bgmGame, 5f)); // 5초 페이드
        // 1. 현재 BGM 볼륨 줄이기
        float startVolume = bgmSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        // 2. 새 BGM 설정
        bgmSource.clip = newClip;
        bgmSource.Play();

        // 3. 새 BGM 볼륨 올리기
        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0f, bgmVolume, time / duration);
            yield return null;
        }

        bgmSource.volume = bgmVolume; // 최종 볼륨 보정
    }
}