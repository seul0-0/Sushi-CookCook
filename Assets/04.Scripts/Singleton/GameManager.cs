using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : Singleton<GameManager>
{
    public int gameLevel {  get;  set; }

    public void AddLevel(int value)
    {
        gameLevel += value;
        Debug.Log($"현재 레벨: {gameLevel}");
    }
}