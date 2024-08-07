using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public void SetMove(List<Node> path)
    {
        StartCoroutine(CoroutineMove(path));
    }

    IEnumerator CoroutineMove(List<Node> path)
    {
        var WaitForEndOfFrame = new WaitForEndOfFrame();

        while (path.Count > 0)
        {
            var dir = path[0].transform.position - transform.position;

            transform.position += dir.normalized * speed * Time.deltaTime;

            if(Vector3.Distance(transform.position, path[0].transform.position) < 0.2f)
            {
                path.RemoveAt(0);
            }

            yield return WaitForEndOfFrame;
        }
    }
}
