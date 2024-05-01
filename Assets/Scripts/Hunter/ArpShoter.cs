using System.Globalization;
using UnityEngine;

public class ArpShoter : SteeringAgent
{
    public void AimToTarget(SteeringAgent targetAgent)
    {
       AddForce(Pursuit(targetAgent));
    }   
    public void ApplyRotation()
    {
        transform.right = _velocity;
    }
    public bool CompareAngle(SteeringAgent targetAgent)
    {
        Vector3 dir = PrePursuit(targetAgent) - transform.position;
        float angle = Vector3.Angle(transform.right, dir);
        return angle < 5;
    }
    public void Shoot()
    {
      //Spawn.Shoot;
    }


}
