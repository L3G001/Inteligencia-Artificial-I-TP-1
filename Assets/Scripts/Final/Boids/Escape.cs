using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : State<Enums.BoidStateID>
{
    Boid boid;
    public Escape(Boid steeringAgent)
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
        FollowMyPath();
    }
    void FollowMyPath()
    {

        if (boid.path.Count > 0)
        {
            if (Vector3.Distance(boid.transform.position, boid.path[0].transform.position) < 2f)
            {
                boid.path.RemoveAt(0);
            }
            else
            {
                boid.AddForce(boid.Seek(boid.path[0].transform.position));
            }
        }
        else
        {
            fsm.ChangeState(Enums.BoidStateID.InBase);
        }
        boid.Spece();
        boid.Move();
    }
}
