using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesEnums : MonoBehaviour
{
    public enum LeaderStateID
    {
        Idle,
        Pathfind,
        Follow,
        OnSight,
        Attack,
        Escape
    }

    public enum NPCStateID
    {
        Idle,
        Pathfind,
        Follow,
        Chase,
        Attack,
        Escape
    }
}
