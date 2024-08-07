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
        Escape
    }

    public enum NPCStateID
    {
        Pathfind,
        Follow,
        Chase,
        Escape
    }
}
