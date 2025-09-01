using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioManager
{
    void PlaySound(string name);
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    public void PlaySound(string name)
    {
        Debug.Log($"{name} 사운드 재생!");
    }
}