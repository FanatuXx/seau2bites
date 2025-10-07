using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NewPlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction move;
    public InputActionReference jump;

    private Rigidbody2D rb;
    public float speed;
    private Vector2 _moveDirection;
    public float jumpForce;
    private bool isRevesed = false;
    private bool isJumping = false;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Animator animator; //anim
    bool facingRight = true; //flip

    bool jumpNow = false;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = playerInputActions.Player.Move;
        move.Enable();

        playerInputActions.Player.Jump.performed += DoJump; 
        playerInputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        playerInputActions.Player.Jump.performed -= DoJump; 
        playerInputActions.Player.Jump.Disable();
    }
    private void DoJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
        jumpNow = true;
    }

    void Flip() //flip
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }


    private void Update()
    {
        Vector2 jumpDir = Vector2.up;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (move.ReadValue<Vector2>().x > 0 && !facingRight) //flip
        {
            Flip();
        }
        if (move.ReadValue<Vector2>().x < 0 && facingRight)
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
                this.isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }

        if (rb.gravityScale <= 0)
        {
            jumpDir = Vector2.down;
        }
        /*if (isGrounded == true && jump.pressed);
        {

            rb.linearVelocity = jumpDir * jumpForce;
            //isJumping = true;
            animator.SetBool("IsJumping", true);

        }
        */

        animator.SetFloat("Speed", Mathf.Abs(move.ReadValue<Vector2>().x)); //anim
    }
    /*  if (!isGrounded)
      {
          this.isJumping = true;
      }

      if (this.isJumping)
      {
          if (isGrounded)
          {

              this.isJumping = false;
              animator.SetBool("IsJumping", false);
          }
      }
    */
    private void FixedUpdate()
    {
        if (jumpNow)
        {
            rb.linearVelocity = new Vector2(move.ReadValue<Vector2>().x * speed, jumpForce);
            jumpNow = false;
        }

        else
        {
            rb.linearVelocity = new Vector2(move.ReadValue<Vector2>().x * speed, rb.linearVelocity.y);
        }

        Debug.Log("Movement values " + move.ReadValue<Vector2>());
        //rb.linearVelocity = new Vector2(move.ReadValue<Vector2>().x * speed, jump.action.ReadValue<float>() * jumpForce); //Movements latéraux

        //rb.linearVelocity = new Vector2(jump.action.ReadValue<float>() * jumpForce, rb.linearVelocity.y); //Jump
        //Debug.Log(jump.action);

        /*float velocity;


        if (this.isRevesed)
        {
            velocity = move.ReadValue<Vector2>().x * speed * -1;
            //this.facingRight = !this.facingRight;
        }
        else
        {
            velocity = move.ReadValue<Vector2>().x * speed;
        }

        rb.linearVelocity = new Vector2(velocity, rb.linearVelocity.y);
        */
    }
}
