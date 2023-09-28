using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the player's transform.

    public float smoothSpeed = 0.125f; // Adjustable smoothness factor.

    private Vector3 offset; // The initial distance between the camera and the player.

    void Start()
    {
        offset = transform.position - target.position; // Calculate the initial offset.
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calculate the desired camera position.

        // Smoothly move the camera towards the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Ensure the camera is always looking at the player.
        transform.LookAt(target);


    }
}
