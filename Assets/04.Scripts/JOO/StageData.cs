using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]   

public class StageData
{
    [Header("스테이지 이름")] 
    public string stageName; // 방금 추가

    [Header("이 스테이지에 등장하는 적들")]
    public EnemyData[] enemies;   // 스테이지 적 리스트
}
