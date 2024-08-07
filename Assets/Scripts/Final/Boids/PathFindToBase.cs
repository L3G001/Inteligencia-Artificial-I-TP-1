using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PathFindToBase : State<Enums.BoidStateID>
{
    Boid _boid;

    public PathFindToBase(Boid boid)
    {
        _boid = boid;
    }

    public override void OnEnter()
    {
        _boid.path = null;
        _boid.path = GameManager.Instance.pathfinding.CalculateTheta(GameManager.Instance.grid.GetNearesNode(_boid.transform.position),GameManager.Instance.grid.GetNearesNode(_boid.RedElseBlue ? GameManager.Instance.boidConfig.RedBase.position : GameManager.Instance.boidConfig.BlueBase.position));
        fsm.ChangeState(Enums.BoidStateID.Escape);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
       
    }
}
