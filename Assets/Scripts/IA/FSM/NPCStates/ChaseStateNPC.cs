using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateNPC : State<StatesEnums.NPCStateID>
{
    NPC _npc;

    public ChaseStateNPC(NPC npc)
    {
        _npc = npc;
    }

    public override void OnEnter() { }
    public override void OnExit() { }

    public override void OnUpdate()
    {
        if (_npc.currentlife <= 10) { fsm.ChangeState(StatesEnums.NPCStateID.Escape); }
        LeaderFlocking();
    }

    void LeaderFlocking()
    {
        var npcs = _npc.redNPC ? GameManagerIA.instance.npcConfig.redAgents : GameManagerIA.instance.npcConfig.blueAgents;
        _npc.AddForce(_npc.Spacing(npcs, GameManagerIA.instance.npcConfig.separationRadius) * GameManagerIA.instance.npcConfig.separationWeight);
        if (_npc.redNPC)
        {
            if(GameManagerIA.instance.InLineOfSight(_npc.transform.position, GameManagerIA.instance.leaderConfig.redLeader.transform.position))
            {
                _npc.AddForce(_npc.Arrive(GameManagerIA.instance.leaderConfig.redLeader.transform.position)*GameManagerIA.instance.npcConfig.arriveWeight);
                _npc.AddForce(_npc.Spacing(GameManagerIA.instance.npcConfig.redAgents, GameManagerIA.instance.npcConfig.separationRadius) * GameManagerIA.instance.npcConfig.separationWeight);
            }
        }
        else
        {
            if (GameManagerIA.instance.InLineOfSight(_npc.transform.position, GameManagerIA.instance.leaderConfig.blueLeader.transform.position))
            {
                _npc.AddForce(_npc.Arrive(GameManagerIA.instance.leaderConfig.blueLeader.transform.position) * GameManagerIA.instance.npcConfig.arriveWeight);
                _npc.AddForce(_npc.Spacing(GameManagerIA.instance.npcConfig.blueAgents, GameManagerIA.instance.npcConfig.separationRadius) * GameManagerIA.instance.npcConfig.separationWeight);
            }
        }
        fsm.ChangeState(StatesEnums.NPCStateID.Pathfind);
    }
}
