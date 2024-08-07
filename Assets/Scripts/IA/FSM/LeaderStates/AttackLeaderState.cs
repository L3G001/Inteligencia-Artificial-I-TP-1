using UnityEngine;

public class AttackLeaderState : State<StatesEnums.LeaderStateID>
{
    Leader _leader;
    public Vector3 lastPosition;
    public Node targetNode;
    private RaycastHit _hit;

    public AttackLeaderState(FSM<StatesEnums.LeaderStateID> _fsm, Leader leader, Node target)
    {
        fsm = _fsm;
        this._leader = leader;
        targetNode = target;
    }

    public override void OnEnter() { }

    public override void OnExit() { }

    public override void OnUpdate()
    {
        if (_leader.currentlife <= 10) { fsm.ChangeState(StatesEnums.LeaderStateID.Escape); }

        var currentBullet = _leader._currentPool.GetObject();
        currentBullet.transform.position = _leader.bulletSpawner.transform.position;
        currentBullet.transform.rotation = _leader.bulletSpawner.transform.rotation;
    }

}
