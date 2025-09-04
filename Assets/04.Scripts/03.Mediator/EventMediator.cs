using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator
{
    private IPlayerData _playerData;

    public EventMediator(IPlayerData playerData)
    {
        _playerData = playerData;
    }

    public void AddGold(int amount)
    {
        _playerData.AddGold(amount);
    }

    public void SpendGold(int amount)
    {
        _playerData.SpendGold(amount);
    }
}
