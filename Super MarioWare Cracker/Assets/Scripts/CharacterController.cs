using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce; 
    private float moveInput;
    private bool isRevesed = false;
    private bool isJumping = false;
    public Volume chromaticab;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator; //anim
    bool facingRight = true; //flip


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chromaticab.enabled = false;

    }


// Update is called once per frame
void Update()
    {
        
    }

   
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        float velocity;

        if (this.isRevesed)
        {
            velocity = moveInput * speed * -1;
        }
        else
        {
            velocity = moveInput * speed;
        }

        rb.linearVelocity = new Vector2(velocity, rb.linearVelocity.y);

        if (moveInput > 0 && !facingRight) //flip

        {
            Flip();
        }

        if (moveInput <0 && facingRight)
        {
             Flip();
        }
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        Vector2 jumpDir = Vector2.up;
        if (rb.gravityScale <= 0)
        {
            jumpDir = Vector2.down;
        }

        if (!isGrounded)
        {
            this.isJumping = true;
        }

        if (this.isJumping)
        {
            if (isGrounded)
            {
                animator.SetBool("IsJumping", false);
                this.isJumping = false;
            }
            
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = jumpDir * jumpForce;
            animator.SetBool("IsJumping", true);
        }



        animator.SetFloat("Speed", Mathf.Abs(moveInput)); //anim
    }


     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Reversegravity"))
        {
            rb.gravityScale *= -1;
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.y *= -1;
            gameObject.transform.localScale = currentScale;
            chromaticab.enabled = true;
            //chromaticab.active = true;
        }
        if (other.gameObject.CompareTag("Reversecommands"))
        {
            this.isRevesed = !this.isRevesed;
            this.facingRight = !this.facingRight;   
        } 
    }

    #region Flip
    void Flip() //flip

    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    #endregion

    

}
