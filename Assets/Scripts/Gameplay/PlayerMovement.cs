using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Rendering;

using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    float moveSpeed;

    [SerializeField] float groundDrag;

    [Header("Jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchYScale;

    [Header("Shrink")]
    [SerializeField] float shrinkXScale;
    [SerializeField] float shrinkYScale;
    [SerializeField] float shrinkZScale;
    float startXScale;
    float startYScale;
    float startZScale;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] KeyCode shrinkKey = KeyCode.Z;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;

    [SerializeField] Transform orientation;

    float horizontalInput;
    float verticalInput;

    public UnityEvent _event;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    bool shrunk;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    public float Speed => rb.velocity.magnitude;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        readyToJump = true;

        startXScale = transform.localScale.x;
        startYScale = transform.localScale.y;
        startZScale = transform.localScale.z;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(shrinkKey))
        {
            shrunk = !shrunk;
            if (shrunk && state != MovementState.crouching)
            {
                transform.localScale = new Vector3(shrinkXScale, shrinkYScale, shrinkZScale);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            else if (shrunk && state == MovementState.crouching)
            {
                transform.localScale = new Vector3(shrinkXScale, shrinkYScale / crouchYScale, shrinkZScale);
                rb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
            }
            else if (!shrunk && state == MovementState.crouching)
            {
                transform.localScale = new Vector3(startXScale, crouchYScale, startZScale);
            }
            else if (!shrunk)
            {
                transform.localScale = new Vector3(startXScale, startYScale, startZScale);
            }
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {   
            if(!shrunk)
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, shrinkYScale, transform.localScale.z);
            }
            
        }

        
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
