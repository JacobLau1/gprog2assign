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
            GameManager gm = new GameManager();
            GameManager2 gm2 = new GameManager2();
            GameManager3 gm3 = new GameManager3();

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    gm.IncrementScore();
                    break;
                case 2:
                    gm2.IncrementScore();
                    break;
                case 3:
                    gm3.IncrementScore();
                    break;
            }

            Debug.Log("Player touched the yellow powerup!");

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
