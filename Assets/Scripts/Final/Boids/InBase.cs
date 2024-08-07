using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBase : State<Enums.BoidStateID>
{
    Boid _boid;
    public InBase(Boid boid)
    {
        _boid = boid;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (_boid.life >= _boid.maxLife) 
        {
            _boid.life = _boid.maxLife;
            fsm.ChangeState(Enums.BoidStateID.PathFindToLead);
        }
        _boid.life += 5 * Time.deltaTime;
    }
}
