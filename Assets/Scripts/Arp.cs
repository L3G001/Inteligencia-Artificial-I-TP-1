using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arp : MonoBehaviour
{
    public Transform mySpawn;
    
    public bool move;
    public float speed;
    void Update()
    {
        if(move)
        {
          transform.position += transform.right *speed* Time.deltaTime;
        }
    }
    void CheckDestroy()
    {

    }
    private void DestroyMeAndResetTarget(SteeringAgent MyTarget)
    {
        MyTarget.ResetPosition();
        Destroy(gameObject);
    }
}
