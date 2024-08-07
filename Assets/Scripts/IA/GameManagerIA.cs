using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerIA : MonoBehaviour
{
    public static GameManagerIA instance;
    public Enemy enemy;
    public LayerMask layerMask, floorLayerMask, obstacleLayerMask;

    public Node redLeaderTarget, blueLeaderTarget;
    public Grid grid;
    public Pathfinding pathfinding;
    public NPCConfig npcConfig;
    public LeaderConfig leaderConfig;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }

    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        var dir = end - start;

        return !Physics.Raycast(start, dir, dir.magnitude, layerMask);
    }
}

[System.Serializable]
public class NPCConfig
{
    public List<SteeringAgent> redAgents = new List<SteeringAgent>();
    public List<SteeringAgent> blueAgents = new List<SteeringAgent>();

    public Node redBase, blueBase;

    public float separationRadius, viewRadius, viewRange;
    public float separationWeight, arriveWeight, obstacleWeight;
    public float maxLife;
}

[System.Serializable]
public class LeaderConfig
{
    public Leader blueLeader, redLeader;
    public float viewRadius, viewRange;
    public float maxLife;
}
