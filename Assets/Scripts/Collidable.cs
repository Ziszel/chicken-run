using System;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private bool _isHit;
    private GameManager _gm;

    private void Start()
    {
        _isHit = false;
        _gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHit)
        {
            if (collision.collider.CompareTag("Player"))
            {
                _gm.UpdateScore(100);
                _isHit = true;
            }
        }
    }
}
