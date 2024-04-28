
using System.Collections.Generic;
using UnityEngine;


public class WaypointManager : MonoBehaviour
{
    [HideInInspector]
    public DebugType debugType;

    public WaypointDebugConfig GeneralConfig;




    public List<Waypoint> Waypoints = new List<Waypoint>();


    private void OnDrawGizmos()
    {
        switch (debugType)
        {
            case DebugType.None:
                break;
            case DebugType.General:
                foreach (Waypoint way in Waypoints)
                {
                    DebugWaypoint(GeneralConfig.GizmoType, GeneralConfig.GizmoRad, way.Position, GeneralConfig.GizmoColor);
                }

                break;
            case DebugType.Singular:
                foreach (Waypoint way in Waypoints)
                {
                    DebugWaypoint(way.SingularConfig.GizmoType, way.SingularConfig.GizmoRad, way.Position, way.SingularConfig.GizmoColor);
                }

                break;
            case DebugType.Mixed:
                foreach (Waypoint way in Waypoints)
                {
                  if (way.UseSingular) DebugWaypoint(way.SingularConfig.GizmoType, way.SingularConfig.GizmoRad, way.Position, way.SingularConfig.GizmoColor);
                  else DebugWaypoint(GeneralConfig.GizmoType, GeneralConfig.GizmoRad, way.Position, GeneralConfig.GizmoColor);
                }
                break;
        }
    }


    private void DebugWaypoint(GizmoType gtype,float rad,Vector3 pos, Color color)
    {
        switch (gtype)
        {
            case GizmoType.Wire:
                Gizmos.color = color;
                Gizmos.DrawWireSphere(pos, rad);
                break;
            case GizmoType.Solid:
                Gizmos.color = color;
                Gizmos.DrawSphere(pos, rad);
                break;
        }



    }




}
[System.Serializable]
public class Waypoint
{
 
    public Vector3 Position;

    public bool UseSingular;
    public WaypointDebugConfig SingularConfig;


}

[System.Serializable]
public class WaypointDebugConfig
{
    public float GizmoRad;
    public Color GizmoColor;
    public GizmoType GizmoType;
}

public enum GizmoType
{ 
  Wire,Solid

}

public enum DebugType
{
    None,General,Singular,Mixed
}


