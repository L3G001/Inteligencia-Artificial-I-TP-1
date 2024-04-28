using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    Hunter myAgent;

    WaypointManager waypointManager;
    int objectiveIndex = 0;

    public Patrol(Hunter myAgent, WaypointManager waypointManager)
    {
        this.myAgent = myAgent;
        this.waypointManager = waypointManager;
        CatchWaypoint();
    }

    public override void OnEnter()
    {
       
    }
    private void PatrolWaypoints()
    {
        if (Vector3.Distance(waypointManager.Waypoints[objectiveIndex].Position, myAgent.transform.position) < 1)
        {
            objectiveIndex++;
            if (objectiveIndex >= waypointManager.Waypoints.Count)
            {
                objectiveIndex = 0;
            }
        }
        myAgent.AddForce(myAgent.Seek(waypointManager.Waypoints[objectiveIndex].Position));
    }
    private void CatchWaypoint()
    {
        Waypoint closerWaypoint = waypointManager.Waypoints[objectiveIndex];
        foreach (Waypoint way in waypointManager.Waypoints)
        {
            if (Vector3.Distance(way.Position, myAgent.transform.position) < Vector3.Distance(closerWaypoint.Position, myAgent.transform.position))
            {
                closerWaypoint = way;
            }
        }
        objectiveIndex = waypointManager.Waypoints.IndexOf(closerWaypoint);
    }
    public override void OnUpdate()
    {
        PatrolWaypoints();
        myAgent.Move();
        myAgent.ChangeFuelPerSecond(-10f);
       
    }

    public override void OnExit()
    {
        
    }

}
