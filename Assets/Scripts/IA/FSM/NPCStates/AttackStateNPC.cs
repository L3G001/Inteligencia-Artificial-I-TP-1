using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateNPC : State<StatesEnums.NPCStateID>
{
    Vector3 lastPosition;
    Node targetNode;
    NPC _npc;

    public AttackStateNPC(NPC npc, Node target)
    {
        _npc = npc;
        targetNode = target;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}
