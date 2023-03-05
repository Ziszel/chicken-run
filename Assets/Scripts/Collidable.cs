using System;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private bool _isHit;

    private void Start()
    {
        _isHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHit)
        {
            if (collision.collider.CompareTag("Player"))
            {
                LevelManager.UpdateScore(100);
                _isHit = true;
            }
        }
    }
}
