using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        { 
            gm.UpdatePickup();
            gameObject.SetActive(false);
        }
    }
}
