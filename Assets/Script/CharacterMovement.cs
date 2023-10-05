using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 gravity;
    public Vector3 playerVelocity;
    public Vector3 move;

    //public bool groundedPlayer;
    public float mouseSensitivy = 5.0f;

    private CharacterController controller;
    private Animator animator;

    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpHeight = 1f;
    public float gravityValue = -9.81f;

    public int jumpsDone = 0;
    public int maxJumps = 1;
    public bool hasDoneFlip = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        ProcessMovement();
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

    void ProcessMovement()
    {
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
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * mouseSensitivy, 0, Space.Self);
    }
    
    void UpdateAnimator()
    {
        bool isGrounded = controller.isGrounded;

        if (!hasDoneFlip && jumpsDone == 2)
        {
            // animator.applyRootMotion = true;
            animator.SetTrigger("ForwardFlip");
            //animator.SetBool("isGrounded", isGrounded);
            hasDoneFlip = true;
        }
        /*
        if (controller.isGrounded)  //to reset the just done the flip so it doesnt redo flip again
        {
            hasDoneFlip = false;
            animator.SetBool("isGrounded", true);
        }
        */

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
           // animator.ResetTrigger("ForwardFlip");
            if (move != Vector3.zero)       //if not staying still
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
            else //staying still
            {
                animator.SetFloat("Speed", 0.0f);
            }
        } else
        {
            animator.SetBool("isGrounded", false);
            if (move != Vector3.zero)       //if not staying still
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
            else //staying still
            {
                animator.SetFloat("Speed", 0.0f);
            }
        }
    }

    public void ProcessGravity()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0.0f)
        {
            playerVelocity.y = -1.0f;
            jumpsDone = 0;
            //animator.SetBool("isGrounded", true);
            hasDoneFlip = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpsDone++;
               // animator.SetBool("isGrounded", isGrounded);
            } else if (jumpsDone < maxJumps)    //doing a double jump
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpsDone++;
                maxJumps = 1;
               // animator.SetBool("isGrounded", isGrounded);
                // doingDoubleJump = true;
            }
        }

        if (!isGrounded) //falling back to ground
        {
           
            playerVelocity.y += gravityValue * Time.deltaTime;
           // animator.SetBool("isGrounded", isGrounded);
        }


        controller.Move(move * Time.deltaTime * GetMovementSpeed() + playerVelocity * Time.deltaTime);
        //animator.SetBool("isGrounded", isGrounded);
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
