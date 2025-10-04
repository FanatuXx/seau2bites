using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NewPlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction move;

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

    private void FixedUpdate()
    {
        Debug.Log("Movement values " + move.ReadValue<Vector2>());
        rb.linearVelocity = new Vector2(move.ReadValue<Vector2>().x * speed, rb.linearVelocity.y);

        //Vector2 moveInput = move.ReadValue<Vector2>();


        //rb.linearVelocity = new Vector2(velocity, rb.linearVelocity.y);
    }

}
