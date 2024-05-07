using System.Collections;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (GameManager.Instance.boidConfig.food.activeSelf == false)
        {
            FoodSpawn();
        }
    }

    void Update()
    {
        if (GameManager.Instance.boidConfig.food.activeSelf == false)
        {
            FoodSpawn();
        }
    }

    void FoodSpawn()
    {
        StartCoroutine(FoodSpawnDelay());
    }

    public void BoidSpawn()
    {
        if (GameManager.Instance.boidConfig.food.activeSelf == false)
        {
            foreach (var agent in GameManager.Instance.boidConfig.allAgents)
            {
                if (agent.gameObject.activeSelf == false)
                {
                    agent.gameObject.SetActive(true);
                    return;
                }
            }
            var newAgent = Instantiate(Resources.Load<GameObject>("Boid"));
            GameManager.Instance.boidConfig.allAgents.Add(newAgent.GetComponent<SteeringAgent>());
            newAgent.transform.position = GameManager.Instance.boidConfig.food.transform.position;
        }
        else return;
    }
    IEnumerator FoodSpawnDelay()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.boidConfig.food.transform.position = GameManager.Instance.foodConfig.waypointManagers.FoodManager.Waypoints[Random.Range(0, GameManager.Instance.foodConfig.waypointManagers.FoodManager.Waypoints.Count)].Position;
        GameManager.Instance.boidConfig.food.gameObject.SetActive(true);
    }
}
