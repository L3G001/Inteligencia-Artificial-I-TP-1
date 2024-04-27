using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Map Configs")]
    [SerializeField] float _boundHeight;
    [SerializeField] float _boundWidth;
    [Header("Hunter Configs")]
    public float hunterCurrentFuel; 
    public float hunterMaxFuel;
    public int hunterGold;
    public int huntedBoids;
    [Header("Boid Configs")]
    public float alignmentWeight = 1;
    public float separationWeight = 1;
    public float cohesionWeight = 1;
    public float separationRadius, cohesionRadius, viewRadius;
    public List<SteeringAgent> allAgents = new List<SteeringAgent>();
    
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
