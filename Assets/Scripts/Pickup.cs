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
            if (gameObject.CompareTag("Pickup"))
            {
                _lm.UpdatePickup();
                gameObject.SetActive(false);   
            }
            else if (gameObject.CompareTag("RedGem"))
            {
                _lm.UpdateRedGem();
                gameObject.SetActive(false);
            }
        }
    }
}
