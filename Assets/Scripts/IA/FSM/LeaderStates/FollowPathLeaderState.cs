using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLeaderState : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;
    RaycastHit _hit;

    public FollowPathLeaderState(Leader leader, Node target)
    {
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter()
    {
        lastPosition = targetNode.transform.position;
        _leader.mat.color = Color.green;
    }

    public override void OnExit()
    {
        _leader.path = null;
    }

    public override void OnUpdate()
    {
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }
        if (targetNode.transform.position != lastPosition) { fsm.ChangeState(StatesEnums.LeaderStateID.Pathfind); }
        FollowPath();
    }

    void FollowPath()
    {
        if (_leader.path.Count > 0)
        {
            if (Vector3.Distance(_leader.transform.position, _leader.path[0].transform.position) < 0.5f)
            {
                _leader.path.RemoveAt(0);
            }
            else
            {
                _leader.AddForce(_leader.Seek(_leader.path[0].transform.position));
            }
        }
        else
        {
            fsm.ChangeState(StatesEnums.LeaderStateID.Idle);
        }
        _leader.Move();
    }
}
