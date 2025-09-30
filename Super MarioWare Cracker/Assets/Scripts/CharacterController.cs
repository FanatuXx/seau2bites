using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class CharacterController : MonoBehaviour
{
    // public InputActionReference move; FOR THE NEW INPUT SYSTEM
    // public InputActionReference jump; FOR THE NEW INPUT SYSTEM

    private float V = 1.5f;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce; 
    private float moveInput;
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
    //public EnemyMovement enemymovement2;

    //public Volume hue;
    //private float hueShiftMin = -180f;
    //private float hueShiftMax = 180f;

    public GameObject ts;
    public GameObject ts2;
    public GameObject ts3;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //bloom.enabled = false;
        //chromaticab.enabled = false;
        ts.SetActive(false);
        ts2.SetActive(false);
        ts3.SetActive(false);

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chromaticab.enabled = false;
        bloom.enabled = false;
        hue.enabled = false;
        hue2.enabled = false;
        hue3.enabled = false;
        //ts.SetActive(false);
        //ts2.SetActive(false);
        //ColorGradingMode = hue.profile.GetSetting<ColorGrading>();

    }

    //public void OnCollisionEnter2D(Collision2D col)
    //{

    //    if (col.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }

    //}


    //public void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Ground"))
    //    {

    //        isGrounded = false;


    //    }


    //}



   
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        //moveInput = move.action.ReadValue<Vector2>().x; FOR THE NEW INPUT SYSTEM
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        Vector2 jumpDir = Vector2.up;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //bool isGrounded1 = GetComponentInChildren<Groundcheck>().isGrounded;
        //isGrounded = isGrounded1;

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

    // FOR THE NEW INPUT SYSTEM
    //private void OnEnable()
    //{
    //    jump.action.started += Jump;
    //}
    //private void OnDisable()
    //{
    //    jump.action.started -= Jump;
    //}

    //private void Jump (InputAction.CallbackContext obj)
    //{
    //    Vector2 jumpDir = Vector2.up;
    //    isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    //    //bool isGrounded1 = GetComponentInChildren<Groundcheck>().isGrounded;
    //    //isGrounded = isGrounded1;

    //    if (!isGrounded)
    //    {
    //        this.isJumping = true;
    //    }

    //    if (this.isJumping)
    //    {
    //        if (isGrounded)
    //        {
    //            animator.SetBool("IsJumping", false);
    //            this.isJumping = false;
    //        }
    //    }

    //    if (rb.gravityScale <= 0)
    //    {
    //        jumpDir = Vector2.down;
    //    }
    //    if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rb.linearVelocity = jumpDir * jumpForce;
    //        animator.SetBool("IsJumping", true);
    //    }


    //    animator.SetFloat("Speed", Mathf.Abs(moveInput)); //anim
    //}


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
            //chromaticab.enabled = true;
            //chromaticab.active = true;
            other.gameObject.SetActive(false);
            hue.enabled = true;
            StartCoroutine(hueshift());
           
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

        if (other.gameObject.CompareTag("Danger"))
        {
            ts.SetActive(true);
        }

        if (other.gameObject.CompareTag("Danger2"))
        {
            ts2.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger3"))
        {
            ts3.SetActive(true);
        }


        if (other.gameObject.CompareTag("SpaceMonkey"))
        { 
            rb.gravityScale = 0.5f;
            bloom.enabled = true;
            other.gameObject.SetActive(false);
            StartCoroutine(ResetGrav(other));
        }

       

        if (other.gameObject.CompareTag("Escargot"))
        {
            //GameObject objectWithScript;
            //objectWithScript.GetComponent<enemymovement>().speed = 0.5f;
            //gameObject = GameObject.FindGameObjectWithTag("RectangleMortel");
            //gameObject.enemymovement.speed = 0.5f;
            enemymovement.speed = 0.5f;
            //enemymovement2.speed = 0.5f;
            ////bloom.enabled = true;
            other.gameObject.SetActive(false);
            ////StartCoroutine(ResetGrav());
        }
    }
    private IEnumerator ResetGrav(Collider2D other)
    {
        yield return new WaitForSeconds(3);
        rb.gravityScale = V;
        bloom.enabled = false;
        other.gameObject.SetActive(true);
    }


    private IEnumerator hueshift()
    {
        while (rb.gravityScale < 0)
        {
            yield return new WaitForSeconds(1);
            hue.enabled = false;
            hue2.enabled = true;
            yield return new WaitForSeconds(1);
            hue2.enabled = false;
            hue3.enabled = true;
            yield return new WaitForSeconds(1);
            hue3.enabled = false;
            hue.enabled = true;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cachette"))
        {
            isHidden = false;
        }

        if (other.gameObject.CompareTag("Danger"))
        {
            ts.SetActive(false);
        }

        if (other.gameObject.CompareTag("Danger2"))
        {
            ts2.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger3"))
        {
            ts3.SetActive(false);
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

    private void OnDrawGizmos()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        Color color = isGrounded ? Color.green : Color.red;
        Gizmos.color = color;
        Gizmos.DrawSphere(feetPos.position, checkRadius);
    }



}
