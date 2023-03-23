using System;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private bool _isHit;
    private ParticleSystem _particle;

    private void Start()
    {
        _isHit = false;
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHit)
        {
            // when a bollard is hit by the player, give the player some score and 'neutralise' the bollard
            if (collision.collider.CompareTag("Player"))
            {
                LevelManager.UpdateScore(100);
                _isHit = true;
                // stop emitting new particles and remove all particles currently emitting
                // https://docs.unity3d.com/ScriptReference/ParticleSystem.Clear.html
                _particle.Stop();
                _particle.Clear();
            }
        }
    }
}
