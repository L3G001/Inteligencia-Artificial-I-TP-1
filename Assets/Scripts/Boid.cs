using System.Collections;
using UnityEngine;

public class Boid : SteeringAgent
{
    public BoidState state;
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
        AddForce(Spacing(boids, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);
        AddForce(Cohesion(boids, GameManager.Instance.boidConfig.cohesionRadius) * GameManager.Instance.boidConfig.cohesionWeight);
        AddForce(Alignment(boids, GameManager.Instance.boidConfig.viewRadius) * GameManager.Instance.boidConfig.alignmentWeight);
    }

    private void UpdateBoundPos() { transform.position = GameManager.Instance.BoundPosition(transform.position); }

    private void Update()
    {
        var target = GameManager.Instance.hunterConfig.hunter;
        var food = GameManager.Instance.boidConfig.food;
        _viewRadius = GameManager.Instance.boidConfig.viewRadius;
        var _separationRadius = GameManager.Instance.boidConfig.separationRadius;
        if (Vector3.Distance(target.transform.position, transform.position) <= _viewRadius)
        {
            AddForce(Evade(target) * GameManager.Instance.boidConfig.evadeWeight);
            state = BoidState.EvadingHunter;
            AddForce(ObstacleAvoidance() * GameManager.Instance.boidConfig.obstacleWeight);
        }
        else if (Vector3.Distance(food.transform.position, transform.position) <= _viewRadius)
        {
            AddForce(Arrive(food.transform.position) * GameManager.Instance.boidConfig.arriveWeight);
            state = BoidState.ArrivingFood;
            StartCoroutine(SpawnDelay());
            AddForce(ObstacleAvoidance() * GameManager.Instance.boidConfig.obstacleWeight);
        }

        if (Vector3.Distance(food.transform.position, transform.position) <= _separationRadius)
        {
            food.gameObject.SetActive(false);
            GameManager.Instance.boidConfig.food.transform.position += new Vector3(0, 0, 20);
            AddForce(ObstacleAvoidance() * GameManager.Instance.boidConfig.obstacleWeight);
        }
        else
        {
            if (HasToUseOA())
            {
                AddForce(ObstacleAvoidance() * GameManager.Instance.boidConfig.obstacleWeight);
            }
            else
            {
                state = BoidState.Flocking;
                Flocking();
            }
        }
        Move();
        UpdateBoundPos();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnDrawGizmos()
    {
        if (GameManager.Instance == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.separationRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.boidConfig.cohesionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Gizmos.color = Color.red;
        Ray ray = new Ray(transform.position + transform.up * 0.5f, transform.right);
        Ray ray2 = new Ray(transform.position - transform.up * 0.5f, transform.right);
        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, _viewRadius, _obstacle)) Gizmos.DrawSphere(hit.point, 0.5f);
        else Gizmos.DrawSphere(ray.origin + ray.direction * _viewRadius, 0.5f);
        if (Physics.SphereCast(ray2, 0.5f, out RaycastHit hit2, _viewRadius, _obstacle)) Gizmos.DrawSphere(hit2.point, 0.5f);
        else Gizmos.DrawSphere(ray2.origin + ray2.direction * _viewRadius, 0.5f);
    }
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(10);
        FoodManager.Instance._oneTimeSpawn = false;
    }
}
public enum BoidState
{
    Flocking,
    EvadingHunter,
    ArrivingFood
}