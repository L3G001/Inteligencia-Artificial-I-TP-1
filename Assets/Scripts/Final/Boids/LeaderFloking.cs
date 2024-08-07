using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFloking : State<Enums.BoidStateID>
{
    Boid boid;
    SteeringAgent Leader;

    public LeaderFloking(Boid steeringAgent)
    {
        boid = steeringAgent;
    }

    public override void OnEnter()
    {
    
    }

    public override void OnExit()
    {
       
    }

    public override void OnUpdate()
    {
       boid.LeadFlocking();
    }
}
