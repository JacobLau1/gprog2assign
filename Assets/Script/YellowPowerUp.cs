using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YellowPowerUp : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;

    public float floatSpeed = 1.0f;  // Speed of floating.
    public float rotationSpeed = 30.0f;  // Speed of spinning.
    public float floatAmplitude = 0.5f; // Amplitude of floating motion.

    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position to use as the reference point for floating.
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Float the object up and down.
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = newPosition;

        // Spin the object.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            GameManager.Score += 50;
            Debug.Log("Player touched the yellowpowerup!");

            Vector3 boxPosition = transform.position;

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
