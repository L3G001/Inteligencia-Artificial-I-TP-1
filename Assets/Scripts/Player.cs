using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    public int speed;
    Vector2 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        dir = new Vector2(moveX, moveY);


    }
    private void FixedUpdate()
    {
        Movement(dir);
    }
    void Movement(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed * Time.fixedDeltaTime;
    }
}
