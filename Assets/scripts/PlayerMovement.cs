using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private float doubleJump = 2;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 1f;
    private float jumpBufferCounter;

    private bool isWallJumping;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            doubleJump = 2f;
        }
        else
        {
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
            else
            {
                coyoteTimeCounter = 0;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
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
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = Mathf.Lerp(rb.gravityScale, 6f, 20 * Time.deltaTime);
        }
        else
        {
            rb.gravityScale = 4f;
        }

    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}