using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateLeader : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;

    public IdleStateLeader(Leader leader, Node target)
    {
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter()
    {
        lastPosition = targetNode.transform.position;
        _leader.mat.color = Color.cyan;
    }

    public override void OnExit() { }

    public override void OnUpdate()
    {
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }
        if (targetNode.transform.position != lastPosition) { fsm.ChangeState(StatesEnums.LeaderStateID.Pathfind); }
    }
}
