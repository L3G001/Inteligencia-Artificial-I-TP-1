using UnityEngine;

public class PathfindLeaderState : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;

    public PathfindLeaderState(Leader leader, Node target)
    {
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter()
    {
        _leader.path = null;
        _leader.path = GameManagerIA.instance.pathfinding.CalculateTheta(GameManagerIA.instance.grid.GetNearestNode(_leader.transform.position), targetNode);
        _leader.mat.color = Color.yellow;
    }

    public override void OnExit() { }
    public override void OnUpdate() 
    {
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }
        if (_leader.path != null) { fsm.ChangeState(StatesEnums.LeaderStateID.Follow); }
    }
}
