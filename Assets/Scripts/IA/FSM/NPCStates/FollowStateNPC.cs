using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowStateNPC : State<StatesEnums.NPCStateID>
{
    Vector3 lastPosition;
    Node targetNode;
    NPC _npc;

    public FollowStateNPC(NPC npc, Node target)
    {
        _npc = npc;
        targetNode = target;
    }

    public override void OnEnter()
    {
        lastPosition = targetNode.transform.position;
    }

    public override void OnExit() { }

    public override void OnUpdate()
    {

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
            fsm.ChangeState(StatesEnums.NPCStateID.Idle);
        }
        _npc.Move();
    }
}
