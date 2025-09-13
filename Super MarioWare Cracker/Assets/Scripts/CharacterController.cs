using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce; 
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public Animator animator; //anim
    bool facingRight = true; //flip
  
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
  
       
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
           // jumpTimeCounter = jumpTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("jumping");
            animator.SetBool("IsJumping", true);

        }




        //    if (jumpTimeCounter > 0)
        //    {
        //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //        jumpTimeCounter -= Time.deltaTime;
        //        animator.SetBool("IsJumping", true); //anim

        //    }
        //    else
        //    {
        //        isJumping = false;
        //        animator.SetBool("IsJumping", false); //anim


        //    }

        //}

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);//anim
            Debug.Log("not jumping");

        }



        animator.SetFloat("Speed", Mathf.Abs(moveInput)); //anim


    }

   
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0 && !facingRight) //flip

        {
            Flip();
        }

        if (moveInput <0 && facingRight)
        {
             Flip();
        }

       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Reversegravity"))
        {
            rb.gravityScale *= -1;
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.y *= -1;
            gameObject.transform.localScale = currentScale;

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
