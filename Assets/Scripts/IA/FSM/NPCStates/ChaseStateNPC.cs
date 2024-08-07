using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateNPC : State<StatesEnums.NPCStateID>
{
    Vector3 lastPosition;
    Node targetNode;
    NPC _npc;

    public ChaseStateNPC(NPC npc, Node target)
    {
        _npc = npc;
        targetNode = target;
    }

    public override void OnEnter()
    {
        lastPosition = targetNode.transform.position;
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}
