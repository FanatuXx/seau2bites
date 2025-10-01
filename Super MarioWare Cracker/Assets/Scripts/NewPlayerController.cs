using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NewPlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction move;

    private float V = 1.5f;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private bool isRevesed = false;
    private bool isJumping = false;
    public Volume chromaticab;
    public Volume bloom;
    public Volume hue;
    public Volume hue2;
    public Volume hue3;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public bool isHidden = false;

    public Animator animator; //anim
    bool facingRight = true; //flip

    public EnemyMovement enemymovement;

    public GameObject ts;
    public GameObject ts2;
    public GameObject ts3;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        ts.SetActive(false);
        ts2.SetActive(false);
        ts3.SetActive(false);
        chromaticab.enabled = false;
        bloom.enabled = false;
        hue.enabled = false;
        hue2.enabled = false;
        hue3.enabled = false;
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
        Vector2 inputVector = move.ReadValue<Vector2>();
    }

}
