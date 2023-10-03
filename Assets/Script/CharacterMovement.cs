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
    //public bool hasDoubleJump = false;
    // private static readonly int ForwardFlipTrigger = Animator.StringToHash("ForwardFlip");
    //private bool isJumpAnimationComplete = true;

    [SerializeField] private float jumpPower;
    private int _numberOfJumps;
    [SerializeField] private int maxNumberOfJumps = 2;


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
        //HandleDoubleJump();
    }

    public void LateUpdate()
    {
        UpdateAnimator();
    }

    void DisableRootMotion()
    {
        animator.applyRootMotion = false;
    }
    /*
    public void isJumpAnimationComplete()
    {
        // Add any additional logic you want for when the jump animation is complete.

        // Reset double jump ability when the jump animation is complete
        hasDoubleJump = true;
    }
    */

    public void ProcessGravity1()
    {
        bool wasGrounded = controller.isGrounded; // Store the previous grounded state

        if (controller.isGrounded)
        {
            if (playerVelocity.y < 0.0f)
            {
                playerVelocity.y = -1.0f;
            }

            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            // Reset double jump ability when grounded
          //  hasDoubleJump = true;
        }
        else
        {
            playerVelocity.y += gravityValue * Time.deltaTime;

            // Check if the character was previously grounded but is no longer
            if (wasGrounded)
            {
                // Character is no longer grounded, set isGrounded to false
                groundedPlayer = false;
            }
        }

        controller.Move(move * Time.deltaTime * GetMovementSpeed() + playerVelocity * Time.deltaTime);
    }
    /*
    public void OnJumpAnimationComplete()
    {
      //  isJumpAnimationComplete = true;
    }
    */
    /*
    private void HandleDoubleJump()
    {
        if (hasDoubleJump && !controller.isGrounded && Input.GetButtonDown("Jump") && isJumpAnimationComplete)
        {
            // Trigger the forward flip animation
            animator.SetTrigger(ForwardFlipTrigger);

            // Apply double jump force
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

            // Disable double jump ability
            hasDoubleJump = false;

            // Set jump animation as not complete
            isJumpAnimationComplete = false;
        }
    }
    */
    /*
    private void HandleDoubleJump()
    {
        if (hasDoubleJump && !controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && isJumpAnimationComplete)
            {
                // Trigger the forward flip animation
                animator.SetTrigger(ForwardFlipTrigger);

                // Apply double jump force
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

                // Disable double jump ability
                hasDoubleJump = false;

                // Set jump animation as not complete
                isJumpAnimationComplete = false;
            }
        }
    }

    */

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
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("doRoll");
        }
        

        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("ForwardFlip"); // Trigger forward flip animation
        }
        
        // Reset the ForwardFlip trigger when transitioning to idle/walk/run
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle/Walk/Run") && animator.GetBool("ForwardFlip"))
        {
            animator.ResetTrigger("ForwardFlip");
        }
        */

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

                _numberOfJumps++;
            }
        }
        else // if not grounded
        {
            if (_numberOfJumps >= maxNumberOfJumps)
            {
                return;
            } else
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
            }
            
        }

        controller.Move(move * Time.deltaTime * GetMovementSpeed() + playerVelocity * Time.deltaTime);

    }
    private bool IsGrounded() => CharacterController.isGrounded;
    private IEnumerator WaitForLanding()
    {
        //ol isGrounded = controller.isGrounded;


        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        _numberOfJumps = 0;
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

