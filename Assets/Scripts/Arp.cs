using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Arp : MonoBehaviour
{
    public Transform mySpawn;

    public float destroyRange;

    public bool move;
    public float speed;
    void Update()
    {
        if(move)
        {
          transform.position += transform.right *speed* Time.deltaTime;
        }
        CheckDestroy();
    }
    void CheckDestroy()
    {
        foreach (SteeringAgent targetAgent in GameManager.Instance.boidConfig.allAgents)
        {
            if (Vector3.Distance(targetAgent.transform.position, transform.position)<destroyRange)
            {
                DestroyMeAndResetTarget(targetAgent);
            }
        }
    }
    private void DestroyMeAndResetTarget(SteeringAgent MyTarget)
    {
        MyTarget.ResetPosition();
        GameManager.Instance.hunterConfig.huntedBoids++;
        GameManager.Instance.hunterConfig.boidsInBoat++;
        Destroy(gameObject);
        Debug.Log("Hunted Boids: " + GameManager.Instance.hunterConfig.huntedBoids);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyRange);
    }
}
