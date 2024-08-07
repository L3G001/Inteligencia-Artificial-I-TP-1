using UnityEngine;

public class IdleLeaderState : State<Enums.LeaderStateID>
{
    LeaderCC steeringAgent;
    public Node targetNode;
    public Vector3 LastPos;


    public IdleLeaderState(FSM<Enums.LeaderStateID> myfsm, LeaderCC steeringAgent, Node targetNode)
    {
        fsm = myfsm;
        this.steeringAgent = steeringAgent;
        this.targetNode = targetNode;
    }

    public override void OnEnter()
    {
        LastPos = targetNode.transform.position;
        steeringAgent.myDebugerMaterial.color = Color.cyan;
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if (targetNode.transform.position != LastPos)
        {
            fsm.ChangeState(Enums.LeaderStateID.PathFind);
        }
    }
}
