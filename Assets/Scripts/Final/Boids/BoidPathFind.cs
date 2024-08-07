using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPathFind : State<Enums.BoidStateID>
{
    Boid boid;
    SteeringAgent Leader;

    public BoidPathFind(Boid boid,SteeringAgent Leader)
    {
        this.boid = boid;
        this.Leader = Leader;
    }

    public override void OnEnter()
    {
        boid.path = null;
        boid.path = GameManager.Instance.pathfinding.CalculateTheta(GameManager.Instance.grid.GetNearesNode(boid.transform.position), GameManager.Instance.grid.GetNearesNode(Leader.transform.position));
        fsm.ChangeState(Enums.BoidStateID.FollowPath);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
