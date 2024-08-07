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
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}
