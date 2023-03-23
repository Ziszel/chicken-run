using System;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public bool onGround = true;
    public bool canBoost = true;
    public TMP_Text boostReadyText;
    public ParticleSystem particle;
    [Header("Movement Parameters")]
    [SerializeField] float speed = 100.0f; // has no effect if using editor
    [SerializeField] private float rotationSpeed = 200.0f;

    [Header("Boost Parameters")] 
    [SerializeField] private float thrust = 10.0f;
    [SerializeField] private float boostCooldown = 5.0f;
    private float _boostCooldownTimer;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (LevelManager.gs == GameState.Playing)
        {
            if (!onGround)
            {
                float xSpeed = Input.GetAxisRaw("Horizontal");
                float zSpeed = Input.GetAxisRaw("Vertical");

                RotatePlayer(xSpeed, zSpeed);
            }

            if (Input.GetKey(KeyCode.Space) && onGround)
            {
                SpeedBoost();
                ResetBoostTimer();
            }

            if (!canBoost)
            {
                _boostCooldownTimer -= Time.deltaTime;
                if (boostReadyText.IsActive())
                {
                    boostReadyText.gameObject.SetActive(false);
                }
            }

            if (_boostCooldownTimer < 0.0f)
            {
                canBoost = true;
                boostReadyText.gameObject.SetActive(true);
            }
        }
        else
        {
            ResetBoostTimer();
            boostReadyText.gameObject.SetActive(false);
        }
        
        // If the player stops moving, stop the particle system
        if (rb.velocity == Vector3.zero || !onGround)
        {
            particle.Stop();
        }
        else
        {
            // All of the particle settings have been pre-configured in the editor so all I need to do here is set
            // the particle system to play
            if (!particle.isPlaying)
            {
                particle.Play();   
            }
        }
        
        // Update particle count / speed dependant on velocity here

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

    private void SpeedBoost()
    {
        if (onGround)
        {
            if (canBoost)
            {
                rb.AddForce(0.0f, 0.0f, thrust, ForceMode.Impulse);
            }
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

    protected void RotatePlayer(float xSpeed, float zSpeed)
    {
        transform.Rotate(zSpeed * rotationSpeed * Time.deltaTime, xSpeed * rotationSpeed * Time.deltaTime, 0.0f);
    }

    private void ResetBoostTimer()
    {
        _boostCooldownTimer = boostCooldown;
        canBoost = false;
    }
}
