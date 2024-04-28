using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : SteeringAgent
{
    private void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        var dir = new Vector3(x, y);
        _velocity = dir.normalized * _maxSpeed;
        GameManager.Instance.boidConfig.allAgents.Add(this);
    }

    private void Flocking()
    {
        var boids = GameManager.Instance.boidConfig.allAgents;
        AddForce(Spacing(boids,GameManager.Instance.boidConfig.separationRadius)*GameManager.Instance.boidConfig.separationWeight);
        AddForce(Cohesion(boids,GameManager.Instance.boidConfig.cohesionRadius)*GameManager.Instance.boidConfig.cohesionWeight);
        AddForce(Alignment(boids,GameManager.Instance.boidConfig.viewRadius)*GameManager.Instance.boidConfig.alignmentWeight);
    }

    private void UpdateBoundPos() { transform.position = GameManager.Instance.BoundPosition(transform.position); }

    private void Update()
    {
        _viewRadius = GameManager.Instance.boidConfig.viewRadius;
        Move();
        Flocking();
        UpdateBoundPos();
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
    }

    private void OnDrawGizmos()
    {
        if(GameManager.Instance == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.separationRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.cohesionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
