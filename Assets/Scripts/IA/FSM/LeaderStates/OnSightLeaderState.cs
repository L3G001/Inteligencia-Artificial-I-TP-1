using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSightLeaderState : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;
    RaycastHit _hit;

    public OnSightLeaderState(FSM<StatesEnums.LeaderStateID> _fsm, Leader leader, Node target)
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
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }
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
}
