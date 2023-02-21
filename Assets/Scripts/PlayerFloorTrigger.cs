using System;
using UnityEngine;

public class PlayerFloorTrigger : MonoBehaviour
{
    public PlayerController pc;
    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            pc.rb.rotation = Quaternion.identity;
            pc.rb.freezeRotation = true;
            pc.onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            pc.rb.freezeRotation = false;
            pc.onGround = false;
            pc.canBoost = false;
        }
    }
}
