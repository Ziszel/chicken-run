using System;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private bool _isHit;
    private GameManager gm;

    private void Start()
    {
        _isHit = false;
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHit)
        {
            if (collision.collider.CompareTag("Player"))
            {
                gm.UpdateScore(100);
                _isHit = true;
            }
        }
    }
}
