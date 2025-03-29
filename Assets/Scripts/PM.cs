using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    public float moveSpeed = 8f;
    public float jumpPower = 20f;
    public bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(2f, 8f);

    private bool isDashing;
    private bool canDash = true;
    private float dashTime = 0.2f;
    private float dashPower = 24f;
    private float dashCooldown = 1f;

    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform headCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask SpikesLayer;
    [SerializeField] private LayerMask EndingLayer;
    [SerializeField] private TrailRenderer tr;

    public GameOver gameManager;
    public Win gameManager2;
    public StarsPoints sp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
        animator.SetBool("Jumping", true);    
        animator.SetBool("Dash", false); 
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.S) && IsGrounded())
        {
            animator.SetBool("CR", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("CR", false);
        }
        

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jumping", true);   
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);   
        }
         
        if (IsGrounded() && rb.velocity.y < 0.1f)
        {
            animator.SetBool("Jumping", false);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Death();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("Wall", true);

        }
        else
        {
            isWallSliding = false;
            animator.SetBool("Wall", false);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("Dash", true);  
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("Dash", false);  
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Death()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, SpikesLayer) || Physics2D.OverlapCircle(wallCheck.position, 0.2f, SpikesLayer) || Physics2D.OverlapCircle(headCheck.position, 0.2f, SpikesLayer))
        {
        gameObject.SetActive(false);
        gameManager.gameOver();
        animator.SetBool("Death", true);
        }
    }

        private void Ending()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, EndingLayer) || Physics2D.OverlapCircle(wallCheck.position, 0.2f, EndingLayer) || Physics2D.OverlapCircle(headCheck.position, 0.2f, EndingLayer))
        {
        gameManager2.nextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Star"))
        {
            sp.stars++;
            Destroy(collider2D.gameObject);
        }
    }
}