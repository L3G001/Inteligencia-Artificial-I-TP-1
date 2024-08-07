using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowPath : State<Enums.LeaderStateID>
{
    LeaderCC leader;
    public Node targetNode;
    public Vector3 LastPos;
    

    public LeaderFollowPath(FSM<Enums.LeaderStateID> myfsm, LeaderCC steeringAgent,Node targetNode)
    {
        fsm = myfsm;
        leader = steeringAgent;
        this.targetNode = targetNode;
        
    }

    public override void OnEnter()
    {
        leader.myDebugerMaterial.color = Color.green;
        LastPos = targetNode.transform.position;
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if (targetNode.transform.position != LastPos) fsm.ChangeState(Enums.LeaderStateID.PathFind);
            FollowMyPath();
    }

    void FollowMyPath()
    {
        
        if (leader.Path.Count > 0)
        {
            if (Vector3.Distance(leader.transform.position, leader.Path[0].transform.position) < 2f)
            {
                leader.Path.RemoveAt(0);
            }
            else
            {
                leader.AddForce(leader.Seek(leader.Path[0].transform.position));
            }
        }
        else
        {
            fsm.ChangeState(Enums.LeaderStateID.Idle);
        }
        leader.Move();
    }
}
