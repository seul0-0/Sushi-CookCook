using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public interface IPlayerData
{
    void AddGold(int amount);
    void SpendGold(int amount);
}

public class PlayerDataM : IPlayerData
{
    //��Ÿ�Ӱ� ����뵥���Ͱ� �ٸ��� ���

    public void AddGold(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void SpendGold(int amount)
    {
        throw new System.NotImplementedException();
    }
}