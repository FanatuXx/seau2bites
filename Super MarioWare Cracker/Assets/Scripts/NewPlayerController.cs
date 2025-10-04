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
    }


    private void Update()
    {
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

        
    }
    private void FixedUpdate()
    {
        Debug.Log("Movement values " + move.ReadValue<Vector2>());
        rb.linearVelocity = new Vector2(move.ReadValue<Vector2>().x * speed, rb.linearVelocity.y); //Movements latéraux

        rb.linearVelocity = new Vector2(jump.action.ReadValue<float>() * jumpForce, rb.linearVelocity.y); //Jump
    }
}
