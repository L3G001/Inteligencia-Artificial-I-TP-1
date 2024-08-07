using UnityEngine;

public class LeaderPathFind : State<Enums.LeaderStateID>
{
    LeaderCC leader;
    public Node targetNode;
    public Vector3 LastPos;


    public LeaderPathFind(FSM<Enums.LeaderStateID> myfsm, LeaderCC steeringAgent,Node targetNode)
    {
        fsm = myfsm;
        leader = steeringAgent;
        this.targetNode = targetNode;
        
    }

    public override void OnEnter()
    {
        leader.Path = null;
        leader.Path = GameManager.Instance.pathfinding.CalculateTheta(GameManager.Instance.grid.GetNearesNode(leader.transform.position), targetNode);
        leader.myDebugerMaterial.color = Color.yellow;

    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        if(leader.Path != null) fsm.ChangeState(Enums.LeaderStateID.FollowPath);
    }
}
