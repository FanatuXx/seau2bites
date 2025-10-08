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
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (move.ReadValue<Vector2>().x > 0 && !facingRight)
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
        animator.SetFloat("Speed", Mathf.Abs(move.ReadValue<Vector2>().x));
    }


    private void FixedUpdate()
    {
        Vector2 jumpDir = Vector2.up;

        // Handle jumping with ground check
        if (jumpNow && isGrounded)
        {
            if (rb.gravityScale <= 0)
            {
                jumpDir = Vector2.down;
            }

            rb.linearVelocity = jumpDir * jumpForce;
            animator.SetBool("IsJumping", true);
            jumpNow = false;
        }

        else
        {
            jumpNow = false; // Reset jumpAudiosource flag even if not grounded
        }

        // Handle horizontal movement
        float velocity;
        if (this.isRevesed)
        {
            velocity = move.ReadValue<Vector2>().x * speed * -1;
        }
        else
        {
            velocity = move.ReadValue<Vector2>().x * speed;
        }

        rb.linearVelocity = new Vector2(velocity, rb.linearVelocity.y);
    }
}
