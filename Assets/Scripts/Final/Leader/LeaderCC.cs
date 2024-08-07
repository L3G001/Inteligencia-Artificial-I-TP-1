using System.Collections.Generic;
using UnityEngine;

public class LeaderCC : SteeringAgent
{
    FSM<Enums.LeaderStateID> _fsm;
    public Material myDebugerMaterial;
    public bool RedElseBlue = false;
    public List<Node> Path;


    // Start is called before the first frame update
    void Start()
    {
        if (RedElseBlue) { GameManager.Instance.boidConfig.RedAgents.Add(this); }
        else { GameManager.Instance.boidConfig.BlueAgents.Add(this); }

        _fsm = new FSM<Enums.LeaderStateID>();

        _fsm.AddState(Enums.LeaderStateID.Idle, new IdleLeaderState(_fsm, this, RedElseBlue ? GameManager.Instance.leaderConfig.redLeaderNode : GameManager.Instance.leaderConfig.blueLeaderNode));
        _fsm.AddState(Enums.LeaderStateID.FollowPath, new LeaderFollowPath(_fsm, this, RedElseBlue ? GameManager.Instance.leaderConfig.redLeaderNode : GameManager.Instance.leaderConfig.blueLeaderNode));
        _fsm.AddState(Enums.LeaderStateID.PathFind, new LeaderPathFind(_fsm, this, RedElseBlue ? GameManager.Instance.leaderConfig.redLeaderNode : GameManager.Instance.leaderConfig.blueLeaderNode));

        _fsm.ChangeState(Enums.LeaderStateID.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnUpdate();
    }
}
