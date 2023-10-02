using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Vector3 gravity;
    public Vector3 playerVelocity;
    Vector3 move;

    public bool groundedPlayer;
    public float mouseSensitivy = 5.0f;

    private CharacterController controller;
    private Animator animator;
   
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpHeight = 1f;
    public float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        ProcessMovement11();
        ProcessGravity();
        UpdateRotation();
    }

    public void LateUpdate()
    {
        UpdateAnimator();
    }

    void DisableRootMotion()
    {
        animator.applyRootMotion = false;
    }

    void ProcessMovement11()
    {
        //good movement and camera
        //just sometimes hard to jump while running
        //also isGrounded is always on?

        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Ignore vertical components
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize to ensure consistent speed
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on input
        Vector3 moveDirection = (cameraForward * Input.GetAxis("Vertical")) + (cameraRight * Input.GetAxis("Horizontal"));

        // Apply the movement direction to the character's transform
        if (moveDirection != Vector3.zero)
        {
            // Normalize the movement direction for consistent speed
            moveDirection.Normalize();

            // Move the character
            controller.Move(moveDirection * GetMovementSpeed() * Time.deltaTime);

            // Set animator parameters for animation
            animator.SetFloat("Speed", moveDirection.magnitude); // Adjust animation speed based on moveDirection magnitude
        //    animator.SetBool("isMoving", true); // Set an "isMoving" parameter for your idle vs. movement animations
        }
        else
        {
            // If no movement input, set animator parameters accordingly
            animator.SetFloat("Speed", 0f);
        //    animator.SetBool("isMoving", false);
        }
    }

    void UpdateRotation()       //rotates player based on mouse
    {
        //transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivy, 0, Space.Self);
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * mouseSensitivy, 0, Space.Self);
    }

    void ProcessMovement()
    {
        // Moving the character forward according to the speed (either walk or run speed)
        float speed = GetMovementSpeed();

        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Make sure to flatten the vectors so that they don't contain any vertical component
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to ensure consistent speed in all directions
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on input and camera orientation
        Vector3 moveDirection = (cameraForward * Input.GetAxis("Vertical")) + (cameraRight * Input.GetAxis("Horizontal"));

        // Apply the movement direction and speed
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            if (Input.GetButtonDown("Jump"))
            {
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            else
            {
                // Dont apply gravity if grounded and not jumping
                gravity.y = -1.0f;
            }
        }
        else
        {
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
        }
        // Apply gravity and move the character
        playerVelocity = gravity * Time.deltaTime + movement;
        controller.Move(playerVelocity);
    }

    void UpdateAnimator()
    {
        bool isGrounded = controller.isGrounded;
        // TODO 
        if (move != Vector3.zero)
        {
            if (GetMovementSpeed() == runSpeed)
            {
                animator.SetFloat("Speed", 1f);
            }
            else
            {
                animator.SetFloat("Speed", 0.5f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }

        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Fire1"))
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("doRoll");
        }

    }

    public void ProcessGravity()
    {
        bool isGrounded = controller.isGrounded;
        // Since there is no physics applied on character controller we have this applies to reapply gravity

        if (isGrounded)
        {
            if (playerVelocity.y < 0.0f) // we want to make sure the players stays grounded when on the ground
            {
                playerVelocity.y = -1.0f;
            }

            if (Input.GetButtonDown("Jump")) // Code to jump
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }
        else // if not grounded
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        controller.Move(move * Time.deltaTime * GetMovementSpeed() + playerVelocity * Time.deltaTime);

    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}

