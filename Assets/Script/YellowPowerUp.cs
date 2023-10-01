using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YellowPowerUp : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            GameManager.Score += 50;
            Debug.Log("Player touched the yellowpowerup!");

            Vector3 boxPosition = transform.position;

            Debug.Log(boxPosition);
           
            //Destroy(gameObject);

            // Create and position the Particle System at the box's location.
            ParticleSystem particles = Instantiate(particleSystemPrefab, boxPosition, Quaternion.identity);

            // Play the particle effect.
            particles.Play();

            // Destroy the yellow box.
            Destroy(gameObject);
            Destroy(particles, 2.0f);
        }
    }

   
}
