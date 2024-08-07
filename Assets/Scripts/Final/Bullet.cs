using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool redElseBlue;
    public float speed = 10;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Boids"))
        {
            if (collision.collider.GetComponent<Boid>().RedElseBlue != redElseBlue)
            {
                collision.collider.GetComponent<Boid>().TakeDamage(40);
            }
        }
    }
}
