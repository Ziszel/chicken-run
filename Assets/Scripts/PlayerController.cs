using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public bool onGround = true;
    [SerializeField] float speed = 100.0f; // has no effect if using editor
    [SerializeField] private float rotationSpeed = 200.0f;

    private void Update()
    {
        if (!onGround)
        {
            float xSpeed = Input.GetAxisRaw("Horizontal");
            float zSpeed = Input.GetAxisRaw("Vertical");
        
            RotatePlayer(xSpeed, zSpeed);
        }
    }

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
        float horizontalAdjustment = rb.velocity.z * 3;
        if (onGround)
        {
            if (zSpeed > 0)
            {
                rb.AddForce(transform.forward * speed);
            }
            else if (zSpeed < 0)
            {
                rb.AddForce(transform.forward * -speed);
            }
        }

        if (xSpeed > 0)
        {
            rb.AddForce(transform.right * (speed + horizontalAdjustment));
        }
        else if (xSpeed < 0)
        {
            rb.AddForce(transform.right * (-speed + -horizontalAdjustment));
        }
    }

    private void RotatePlayer(float xSpeed, float zSpeed)
    {
        transform.Rotate(zSpeed * rotationSpeed * Time.deltaTime, xSpeed * rotationSpeed * Time.deltaTime, 0.0f);
    }
}
