using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePowerUp : MonoBehaviour
{

    public ParticleSystem particleSystemPrefab;

    public float floatSpeed = 1.0f;  // Speed of floating.
    public float rotationSpeed = 30.0f;  // Speed of spinning.
    public float floatAmplitude = 0.5f; // Amplitude of floating motion.

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
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

  
}
