using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheckpoint, groundCheckpoint2;
    public LayerMask whatIsGround;
    public Animator anim;

    public float speed = 6;
    public float jumpForce = 8;
    public float fastFallSpeed = 9;
    private bool isGrounded = false;
    private int jumpCount = 0;
    public SpriteRenderer sr;
    public GameMenu gm;

    public float hangTime = 0.2f;
    private float hangCounter;

    public float jumpBufferLength = 0.2f;
    private float jumpBufferCounter;

    public AudioSource jumpSound;
    public AudioSource landSound;
    public AudioSource smashSound;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, 0.05f, whatIsGround) || Physics2D.OverlapCircle(groundCheckpoint2.position, 0.05f, whatIsGround);

        // Manage hangtime + reset jumpcount 
        if (isGrounded)
        {
            hangCounter = hangTime;
            jumpCount = 0;
        } else
        {
            hangCounter -= Time.deltaTime;
        }

        // Manage double jump
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount == 1)
        {
            if (jumpSound.enabled == true)
            {
                jumpSound.Play();
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount += 1;
        }

        // Manage jumpBuffer
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpBufferCounter = jumpBufferLength;
        } else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter >= 0 && hangCounter > 0)
        {
            if (jumpSound.enabled == true)
            {
                jumpSound.Play();
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
        }

        // Jump height
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpCount += 1;
        }

        // Fastfall
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed);
        }

        // Flips player sprite
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sr.flipX = false;
        } else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sr.flipX = true;
        }

        // Handle animation states
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") == 0) {
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", false);
            } else
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isJumping", false);
            }
        } else
        {
            if (jumpCount == 2)
            {
                // if you just double jumped we have another jumping animation
                anim.Play("Jump", -1, 0f);
            }
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", true);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (landSound.enabled == true)
            {
                landSound.Play();
            }
        } else if (collision.gameObject.CompareTag("Enemy")) {
            if (collision.gameObject.GetComponent<Boulder>().isActive)
            {
                if (smashSound.enabled == true)
                {
                    smashSound.Play();
                }
                sr.enabled = false;
                gm.gameOver = true;
            }
        } else if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Bottom"))
        {
            if (smashSound.enabled == true)
            {
                smashSound.Play();
            }
            sr.enabled = false;
            gm.gameOver = true;
        }
    }
}