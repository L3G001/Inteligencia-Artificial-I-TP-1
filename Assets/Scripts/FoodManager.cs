using System.Collections;
using System.Net;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
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
        BoidSpawn();
    }

    void FoodSpawn()
    {
        FoodSpawnDelay();
        GameManager.Instance.boidConfig.food.transform.position = GameManager.Instance.foodConfig.waypointManagers.FoodManager.Waypoints[Random.Range(0, GameManager.Instance.foodConfig.waypointManagers.FoodManager.Waypoints.Count)].Position;
        GameManager.Instance.boidConfig.food.gameObject.SetActive(true);
    }

    void BoidSpawn()
    {
        if (GameManager.Instance.boidConfig.food.activeSelf == false)
        {
            foreach(var agent in GameManager.Instance.boidConfig.allAgents)
            {
                var inactive = 0;
                if (agent.gameObject.activeSelf == false)
                {
                    inactive++;
                    return;
                }
                if(inactive == 0)
                {
                    Instantiate(GameManager.Instance.boidConfig.boidPrefab);
                    GameManager.Instance.boidConfig.allAgents.Add(GameManager.Instance.boidConfig.boidPrefab.GetComponent<SteeringAgent>());
                }
                else
                {
                    agent.gameObject.SetActive(true);
                    inactive--;
                }
            }
        }
        else return;
    }
    IEnumerator FoodSpawnDelay()
    {
        yield return new WaitForSeconds(2);
    }
}
