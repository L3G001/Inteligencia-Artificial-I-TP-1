using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeStateNPC : State<StatesEnums.NPCStateID>
{
    Vector3 lastPosition;
    Node targetNode;
    NPC _npc;

    public EscapeStateNPC(NPC npc, Node target)
    {
        _npc = npc;
        targetNode = target;
    }

    public override void OnEnter()
    {
        lastPosition = targetNode.transform.position;
        _npc.path = null;
        _npc.path = GameManagerIA.instance.pathfinding.CalculateTheta(GameManagerIA.instance.grid.GetNearestNode(_npc.transform.position), targetNode);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (_npc.path.Count > 0)
        {
            if (Vector3.Distance(_npc.transform.position, _npc.path[0].transform.position) < 0.1f)
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
            if (_npc.currentlife >= GameManagerIA.instance.npcConfig.maxLife) { fsm.ChangeState(StatesEnums.NPCStateID.Chase); }
        }
        _npc.Move();
    }
}
