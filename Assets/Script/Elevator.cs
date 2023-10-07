using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;   
    public float moveSpeed = 5f; 

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position for the elevator
        Vector3 targetPosition = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.PingPong(Time.time * moveSpeed, 1f));

        // Move the elevator to the new position
        transform.position = targetPosition;
    }
}
