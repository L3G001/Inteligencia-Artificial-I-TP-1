using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowStateNPC : State<StatesEnums.NPCStateID>
{
    NPC _npc;

    public FollowStateNPC(NPC npc)
    {
        _npc = npc;
    }

    public override void OnEnter() { }
    public override void OnExit() { }

    public override void OnUpdate()
    {
        if (_npc.currentlife <= 10) { fsm.ChangeState(StatesEnums.NPCStateID.Escape); }
        FollowPath();
    }

    void FollowPath()
    {
        if (_npc.path.Count > 0)
        {
            if (Vector3.Distance(_npc.transform.position, _npc.path[0].transform.position) < 0.5f)
            {
                _npc.path.RemoveAt(0);
            }
            else
            {
                _npc.AddForce(_npc.Seek(_npc.path[0].transform.position));
            }
        }
        else
        {
            fsm.ChangeState(StatesEnums.NPCStateID.Chase);
        }
        _npc.AddForce(_npc.Spacing(_npc.redNPC ? GameManagerIA.instance.npcConfig.redAgents : GameManagerIA.instance.npcConfig.blueAgents, GameManagerIA.instance.npcConfig.separationRadius) * GameManagerIA.instance.npcConfig.separationWeight);
        _npc.Move();
    }
}
