using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator
{
    private PlayerDataM _playerData;  // IPlayerData 대신 구체 클래스 사용

    public EventMediator(PlayerDataM playerData)
    {
        _playerData = playerData;
    }



}
