using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeLeaderState : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;

    public EscapeLeaderState(Leader leader, Node target)
    {
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter()
    {
        lastPosition = targetNode.transform.position;
        _leader.path = null;
        _leader.path = GameManagerIA.instance.pathfinding.CalculateTheta(GameManagerIA.instance.grid.GetNearestNode(_leader.transform.position), targetNode);
        _leader.mat.color = Color.red;
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
        if (_leader.path.Count > 0)
        {
            if (Vector3.Distance(_leader.transform.position, _leader.path[0].transform.position) < 0.1f)
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
