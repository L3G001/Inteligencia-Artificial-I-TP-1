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
        GameManager.Instance.allAgents.Add(this);
    }

    private void Flocking()
    {
        var boids = GameManager.Instance.allAgents;
        AddForce(Spacing(boids,GameManager.Instance.separationRadius)*GameManager.Instance.separationWeight);
        AddForce(Cohesion(boids,GameManager.Instance.cohesionRadius)*GameManager.Instance.cohesionWeight);
        AddForce(Alignment(boids,GameManager.Instance.viewRadius)*GameManager.Instance.alignmentWeight);
    }

    private void UpdateBoundPos() { transform.position = GameManager.Instance.BoundPosition(transform.position); }

    private void Update()
    {
        _viewRadius = GameManager.Instance.viewRadius;
        Move();
        Flocking();
        UpdateBoundPos();
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
    }

    private void OnDrawGizmos()
    {
        if(GameManager.Instance == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.separationRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.cohesionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
