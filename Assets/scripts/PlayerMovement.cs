using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isPiped = false;

    private int doubleJump = 2;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 1f;
    private float jumpBufferCounter;
    
    private bool controlsEnabled = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    public void DisableControls()
    {
        controlsEnabled = false;
        doubleJump = 0;
        coyoteTimeCounter = 0;
    }
    
    public void EnableControls() {controlsEnabled = true;}
    
    private void FixedUpdate()
    {
        if (!controlsEnabled) return;
        
        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            doubleJump = 2;
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

        if (Input.GetButtonDown("Jump"))
        {
            print("Jumped");
            jumpBufferCounter = jumpBufferTime;
            if (jumpBufferCounter > 0)
            {
                jumpBufferCounter -= Time.deltaTime;
                if (coyoteTimeCounter > 0f || doubleJump > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                    doubleJump -= 1;
                }
            }
            else
            {
                jumpBufferCounter = 0;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        rb.gravityScale = rb.velocity.y < 0f ? Mathf.Lerp(rb.gravityScale, 4f, 20 * Time.deltaTime) : 4f;

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}