using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateLeader : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;
    RaycastHit _hit;

    public IdleStateLeader(FSM<StatesEnums.LeaderStateID> _fsm, Leader leader, Node target)
    {
        fsm = _fsm;
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter()
    {
        _leader.transform.position = targetNode.transform.position;
    }

    public override void OnExit() { }

    public override void OnUpdate()
    {
        _leader.mat.color = Color.cyan;
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }
        if (targetNode.transform.position != lastPosition) { fsm.ChangeState(StatesEnums.LeaderStateID.Pathfind); }
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
