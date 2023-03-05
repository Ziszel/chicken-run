using UnityEngine;

public class Pickup : MonoBehaviour
{
    private LevelManager _lm;
    private void Start()
    {
        _lm = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            _lm.UpdatePickup();
            gameObject.SetActive(false);
        }
    }
}
