using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Map Configs")]
    [SerializeField] float _boundHeight;
    [SerializeField] float _boundWidth;
    public HunterConfig hunterConfig;
    public BoidConfig boidConfig;
    public FoodConfig foodConfig;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3 (_boundWidth,_boundHeight));
    }

    public Vector3 BoundPosition (Vector3 pos)
    {
        float height = _boundHeight / 2;
        float width = _boundWidth / 2;
        if (pos.y > height) pos.y = -height;
        if (pos.y < -height) pos.y = height;
        if (pos.x > width) pos.x = -width;
        if (pos.x < -width) pos.x = width;

        return pos;
    }
}
[System.Serializable]
public class BoidConfig
{
    public float alignmentWeight = 1;
    public float separationWeight = 1;
    public float cohesionWeight = 1;
    public float separationRadius, cohesionRadius, viewRadius;
    public float evadeWeight = 1;
    public float arriveWeight = 1;
    public float obstacleWeight = 1;
    public List<SteeringAgent> allAgents = new List<SteeringAgent>();
    public GameObject food;
    public GameObject boidPrefab;
}
[System.Serializable]
public class HunterConfig
{
    [Header("ShootConfig")]
    public float shootRadius;

    [Header("FuelConfig")]
    public float hunterCurrentFuel;
    public float hunterMaxFuel;
    public float hunterPatrolCost;
    public float hunterShootCost;

    [Header("HuntProf")]
    public int hunterGold;
    public int huntedBoids;
    public int boidsInBoat;
    public float sellTime;
    public int duckPrice;

    public Hunter hunter;

    public WaypointManagers waypointManagers;
}
[System.Serializable]
public class FoodConfig
{
    public WaypointManagers waypointManagers;
}

[System.Serializable]
public class WaypointManagers
{
    public WaypointManager PatrolManager;
    public WaypointManager FoodManager;
    public WaypointManager DockManager;
}
