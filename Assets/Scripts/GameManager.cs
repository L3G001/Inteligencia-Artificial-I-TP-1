using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Pathfinding pathfinding;

    public MyGrid grid;
    public LeaderConfig leaderConfig;
    public BoidConfig boidConfig;
    public LayerMask FloorMask;

    [InspectorName("MapLayerMask")]public LayerMask layerMask;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        var dir = end - start;

        return !Physics.Raycast(start, dir, dir.magnitude, layerMask);
    }
    public bool InLineOfSight(Vector3 start, Vector3 end,Collider MyColider,Collider OtherColither)
    {
        var dir = end - start;
         
        if(Physics.Raycast(start, dir, out RaycastHit hit ,dir.magnitude, layerMask))
        {
            return hit.collider == MyColider || hit.collider == OtherColither;
        }
        else
        {
            return true;
        }

    }












}
[System.Serializable]
public class BoidConfig
{
    public List<SteeringAgent> BlueAgents = new List<SteeringAgent>();
    public List<SteeringAgent> RedAgents = new List<SteeringAgent>();

    public Transform BlueBase,RedBase;

    public float separationWeight = 1;
  
    public float separationRadius, viewRadius;

    public float evadeWeight = 1;

    public float arriveWeight = 1;

    public float obstacleWeight = 1;
    
}
[System.Serializable]
public class LeaderConfig
{
    
    public SteeringAgent blueLeader;
    public SteeringAgent redLeader;

    public Node blueLeaderNode;
    public Node redLeaderNode;
       
}


