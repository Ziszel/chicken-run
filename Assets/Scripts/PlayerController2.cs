using UnityEngine;

public class PlayerController2 : PlayerController
{

    [Header("Jump Parameters")] 
    [SerializeField] private float force = 50.0f;
    [SerializeField] private float jumpCooldown = 5.0f;
    private float _jumpCooldownTimer;

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
                Jump();
                ResetJumpTimer();
            }

            if (!canBoost)
            {
                _jumpCooldownTimer -= Time.deltaTime;
                if (boostReadyText.IsActive())
                {
                    boostReadyText.gameObject.SetActive(false);
                }
            }

            if (_jumpCooldownTimer < 0.0f)
            {
                canBoost = true;
                boostReadyText.gameObject.SetActive(true);
            }
        }
        else
        {
            ResetJumpTimer();
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
        
        if (Input.GetKey(KeyCode.Escape))
        {
            // not ideal opposing this method to every class at all times, but fine for a prototype / demo.
            LevelManager.RestartLevel();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0.0f, force, 0.0f), ForceMode.Impulse);
        
    }

    private void ResetJumpTimer()
    {
        _jumpCooldownTimer = jumpCooldown;
        canBoost = false;
    }
}
