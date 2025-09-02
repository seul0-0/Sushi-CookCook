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

    public EventMediator mediator;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Mediator ���� (PlayerDataM�� IPlayerData�� ����)
        PlayerStatsTest statsData = Resources.Load<PlayerStatsTest>("PlayerStats");
        mediator = new EventMediator(new PlayerDataM(statsData));
    }

    public void AddLevel(int value)
    {
        gameLevel += value;
    }

    //�������� �������� ������ ����
}