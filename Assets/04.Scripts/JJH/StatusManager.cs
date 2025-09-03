using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    // === PlayerStatus 스크립터블 오브젝트를 받음 ===
    public PlayerStatus status;

    public Sprite[] staticons;                         // === icon을 미리 할당 ===
}
