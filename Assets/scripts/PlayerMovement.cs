using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8f;
    private float jumpingPower = 12f;
    public bool facingRight; // Track the direction the player is facing

    public int doubleJump = 1;

    private float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    private float jumpBufferTime = 1f;
    private float jumpBufferCounter;
    
    private bool controlsEnabled = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private SmoothPlayerCam smoothCam;
    private float fallSpeedYDampingChangeThreshold;

    private void Start()
    {
        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;
    }

    public void DisableControls()
    {
        controlsEnabled = false;
        doubleJump = 0;
        coyoteTimeCounter = 0;
    }
    
    public void EnableControls() {controlsEnabled = true;}

    private void Update()
    {
        if (rb.velocityY < fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        
        if (rb.velocityY >= 0f && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }
    }

    private void FixedUpdate()
    {
        if (!controlsEnabled) return;

        float moveDirection = Input.GetAxis("Horizontal");

        // Flip the player based on the direction of movement
        if (moveDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && facingRight)
        {
            Flip();
        }

        if (doubleJump <= 0)
        {
            doubleJump = 0;
        }

        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            doubleJump = 1;
        }
        else
        {
            switch (coyoteTimeCounter)
            {
                case > 0:
                    coyoteTimeCounter -= Time.deltaTime;
                    break;
                case < 0:
                    coyoteTimeCounter = 0;
                    break;
            }
        }

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            jumpBufferCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Jump"))
        {
            if (jumpBufferCounter > 0f)
            {
                if (coyoteTimeCounter > 0f || doubleJump > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                    doubleJump -= 1;
                }
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
        }
        rb.gravityScale = rb.velocity.y < 0f ? Mathf.Lerp(rb.gravityScale, 4f, 20 * Time.deltaTime) : 4f;
    }
    void Flip()
    {
        // Toggle the direction the player is facing
        facingRight = !facingRight;

        // Rotate the player 180 degrees around the Y-axis
        var rotation = transform.eulerAngles;
        rotation.y += 180;
        transform.eulerAngles = rotation;
        smoothCam.CallTurn();
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}