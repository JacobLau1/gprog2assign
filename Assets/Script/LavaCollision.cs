using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollision : MonoBehaviour
{
    public Transform respawnPoint; // Set this in the Inspector to a GameObject representing the respawn point.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust the tag as needed.
        {
            // Reset the player's position to the respawn point.
            other.transform.position = respawnPoint.position;
        }
    }
}
