using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class CharacterController : MonoBehaviour
{
    private const float V = 1.5f;
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

    public bool isHidden = false;

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
        moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 jumpDir = Vector2.up;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (moveInput > 0 && !facingRight) //flip

        {
            Flip();
        }

        if (moveInput < 0 && facingRight)
        {
            Flip();
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

        if (rb.gravityScale <= 0)
        {
            jumpDir = Vector2.down;
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = jumpDir * jumpForce;
            animator.SetBool("IsJumping", true);
        }





        animator.SetFloat("Speed", Mathf.Abs(moveInput)); //anim
    }

   
    void FixedUpdate()
    {
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
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Reversecommands"))
        {
            this.isRevesed = !this.isRevesed;
            this.facingRight = !this.facingRight;   
        }

        if (other.gameObject.CompareTag("Cachette"))
        {
            isHidden = true;
        }

        if (other.gameObject.CompareTag("SpaceMonkey"))
        { 
            rb.gravityScale = 0.5f;
            StartCoroutine(ResetGrav());
            other.gameObject.SetActive(false);
        }

        IEnumerator ResetGrav()
        {
            yield return new WaitForSeconds(3);
            rb.gravityScale = V;
            other.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cachette"))
        {
            isHidden = false;
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
