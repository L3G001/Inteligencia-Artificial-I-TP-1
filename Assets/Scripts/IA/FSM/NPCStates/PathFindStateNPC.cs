using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindStateNPC : State<StatesEnums.NPCStateID>
{
    NPC _npc;
    Leader _leader;

    public PathFindStateNPC(NPC npc, Leader lead)
    {
        _npc = npc;
        _leader = lead;
    }

    public override void OnEnter()
    {
        _npc.path = null;
        _npc.path = GameManagerIA.instance.pathfinding.CalculateTheta(GameManagerIA.instance.grid.GetNearestNode(_npc.transform.position), GameManagerIA.instance.grid.GetNearestNode(_leader.transform.position));
        fsm.ChangeState(StatesEnums.NPCStateID.Follow);
    }

    public override void OnExit() { }
    public override void OnUpdate() { }
}
