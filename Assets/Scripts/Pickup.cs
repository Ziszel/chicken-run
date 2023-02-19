using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            gm.UpdatePickup();
            gameObject.SetActive(false);
        }
    }
}
