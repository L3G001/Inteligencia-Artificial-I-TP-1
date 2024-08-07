using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (collision.collider.GetComponent<Boid>().RedElseBlue == false)
            {
                collision.collider.GetComponent<Boid>().TakeDamage(40);
            }
        } 
    }
}
