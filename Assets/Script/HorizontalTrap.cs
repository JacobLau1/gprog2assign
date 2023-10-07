using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTrap : MonoBehaviour
{

    public Transform startPoint; // The starting point of the trap's movement
    public Transform endPoint;   // The ending point of the trap's movement
    public float moveSpeed = 5f; // The speed at which the trap moves

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position for the trap
        Vector3 targetPosition = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.PingPong(Time.time * moveSpeed, 1f));

        // Move the trap to the new position
        transform.position = targetPosition;
    }
}
