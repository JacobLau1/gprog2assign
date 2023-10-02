using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    /*
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake() => _offset = transform.position - target.position;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
    */

    // Start is called before the first frame update
    public float speed = 10.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        //  float verticalMove = Input.GetAxis("Vertical");
        //float mouseX = Input.GetAxis("Mouse X");
        float verticalMove = 0;
        //transform.position = transform.position + new Vector3(horizontalMove, 0, verticalMove) * Time.deltaTime;
        transform.Translate(new Vector3(horizontalMove, 0, verticalMove) * speed * Time.deltaTime);
        //transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
