using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public interface IGameManager
{
    void AddLevel(int value);
}

public class GameManager : MonoBehaviour, IGameManager
{
    public int gameLevel {  get;  set; }

    public void AddLevel(int value)
    {
        gameLevel += value;
    }
}