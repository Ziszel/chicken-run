using UnityEngine;

public class PlayerFloorTrigger : MonoBehaviour
{
    [HideInInspector]
    public PlayerController pc;
    private int _flips;
    private int _baseScore;
    private float _totalRotation = 0.0f;
    private Quaternion _lastRotation;
    private void Start()
    {
        _flips = 0;
        _baseScore = 1;
        pc = GetComponentInParent<PlayerController>();
    }

    private void InitialiseFlipCounter()
    {
        _totalRotation = 0.0f; // reset tracked rotations
        _lastRotation = pc.transform.rotation;
    }

    private void Update()
    {
        if (pc.onGround)
        {
            InitialiseFlipCounter();
        }
        
        if (!pc.onGround)
        {
            // this is currently borked. Will investigate later to perfect score
            // https://forum.unity.com/threads/get-the-difference-between-two-quaternions-and-add-it-to-another-quaternion.513187/
            // https://www.reddit.com/r/Unity3D/comments/o0yyz6/how_to_count_car_flips/
            Quaternion rotationDifference = pc.transform.rotation * Quaternion.Inverse(_lastRotation);
            _totalRotation += rotationDifference.eulerAngles.x;

            if (_totalRotation > 360f)
            {
                _flips += 1;
                InitialiseFlipCounter();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            pc.onGround = true;
            pc.rb.rotation = Quaternion.identity;
            pc.rb.freezeRotation = true;
            if (_flips > 0)
            {
                GameManager.UpdateScore(_baseScore * _flips);
            }
            //Debug.Log(_flips);
            _flips = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            InitialiseFlipCounter();
            pc.rb.freezeRotation = false;
            pc.onGround = false;
            pc.canBoost = false;
        }
    }
}
