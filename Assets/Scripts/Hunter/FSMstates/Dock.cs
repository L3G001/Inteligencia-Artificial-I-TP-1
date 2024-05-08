using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : State
{
    Hunter myAgent;

    WaypointManager waypointManager;
    int objectiveIndex = 0;

    public Dock(Hunter myAgent, WaypointManager waypointManager)
    {
        this.myAgent = myAgent;
        this.waypointManager = waypointManager;
    }
    public override void OnEnter()
    {
        Waypoint closerWaypoint = waypointManager.Waypoints[objectiveIndex];
        foreach (Waypoint way in waypointManager.Waypoints)
        {
            if (Vector3.Distance(way.Position, myAgent.transform.position) < Vector3.Distance(closerWaypoint.Position, myAgent.transform.position))
            {
                closerWaypoint = way;
            }
        }
        if(waypointManager.Waypoints.IndexOf(closerWaypoint) == waypointManager.Waypoints.Count - 1)
        {
            objectiveIndex = waypointManager.Waypoints.Count - 2;
        }
        else
        {

        objectiveIndex = waypointManager.Waypoints.IndexOf(closerWaypoint);
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        myAgent.Move();
        if (Vector3.Distance(waypointManager.Waypoints[objectiveIndex].Position, myAgent.transform.position) < 1)
        {
            objectiveIndex++;
            if (objectiveIndex >= waypointManager.Waypoints.Count)
            {
                fsm.ChangeState(StateID.Selling);
            }
        }
        myAgent.AddForce(myAgent.Seek(waypointManager.Waypoints[objectiveIndex].Position));
    }
}
