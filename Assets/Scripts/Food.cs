using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnDisable()
    {
        foreach (var agent in GameManager.Instance.boidConfig.allAgents)
        {
            if (agent.gameObject.activeSelf == false)
            {
                agent.transform.position = GameManager.Instance.boidConfig.food.transform.position;
                agent.gameObject.SetActive(true);
                return;
            }
        }
        var newAgent = Instantiate(Resources.Load<GameObject>("Boid"));
        GameManager.Instance.boidConfig.allAgents.Add(newAgent.GetComponent<SteeringAgent>());
        newAgent.transform.position = GameManager.Instance.boidConfig.food.transform.position;
    }
}
