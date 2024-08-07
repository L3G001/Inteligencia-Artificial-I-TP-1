using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLeaderState : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;
    RaycastHit _hit;

    public FollowPathLeaderState(FSM<StatesEnums.LeaderStateID> _fsm, Leader leader, Node target)
    {
        fsm = _fsm;
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        _leader.path = null;
    }

    public override void OnUpdate()
    {
        _leader.mat.color = Color.green;
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }
        if (targetNode.transform.position == lastPosition)
        {
            if (GameManagerIA.instance.InLineOfSight(_leader.transform.position, targetNode.transform.position)) { fsm.ChangeState(StatesEnums.LeaderStateID.OnSight); }
            else { FollowPath(); }
        }
        else
        {
            if (GameManagerIA.instance.InLineOfSight(_leader.transform.position, targetNode.transform.position)) { fsm.ChangeState(StatesEnums.LeaderStateID.OnSight); }
            else { fsm.ChangeState(StatesEnums.LeaderStateID.Pathfind); }
        }
        if ((Physics.SphereCast(_leader.transform.position, GameManagerIA.instance.leaderConfig.viewRadius, _leader.transform.TransformDirection(Vector3.forward), out _hit, GameManagerIA.instance.leaderConfig.viewRange)))
        {
            if (_hit.collider.TryGetComponent(out NPC npc))
            {
                if (_leader.redLeader && !npc.redNPC)
                {
                    fsm.ChangeState(StatesEnums.LeaderStateID.Attack);
                }
            }
            else if (_hit.collider.TryGetComponent(out Leader lead))
            {
                if (_leader.redLeader && !lead.redLeader)
                {
                    fsm.ChangeState(StatesEnums.LeaderStateID.Attack);
                }
            }
        }
    }

    void FollowPath()
    {
        if (_leader.path.Count > 0  )
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
