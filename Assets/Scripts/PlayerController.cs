using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] float speed = 100.0f; // has no effect if using editor

    private void FixedUpdate()
    {
        float xSpeed = Input.GetAxisRaw("Horizontal");
        float zSpeed = Input.GetAxisRaw("Vertical");

        if (xSpeed != 0 || zSpeed != 0)
        {
            MovePlayer(xSpeed, zSpeed);
        }
    }

    private void MovePlayer(float xSpeed, float zSpeed)
    {
        if (zSpeed > 0)
        {
            rb.AddForce(transform.forward * speed);
        }
        else if (zSpeed < 0)
        {
            rb.AddForce(transform.forward * -speed);
        }

        if (xSpeed > 0)
        {
            rb.AddForce(transform.right * speed);
        }
        else if (xSpeed < 0)
        {
            rb.AddForce(transform.right * -speed);
        }
        
    }
}
