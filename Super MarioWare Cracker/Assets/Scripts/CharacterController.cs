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
    public Volume saturation;
    public Volume film;


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
    public GameObject ts4;
    public GameObject ts5;
    public GameObject ts6;
    public GameObject ts7;
    public GameObject ts8;
    public GameObject ts9;
    public GameObject ts10;
    public GameObject ts11;
    public GameObject ts12;
    public GameObject ts13;
    public GameObject ts14;
    public GameObject ts15;
    public GameObject ts16;
    public GameObject ts17;

    public AudioSource pill;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip myAudioClip;
    //public AudioSource walk;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //bloom.enabled = false;
        //chromaticab.enabled = false;
        ts.SetActive(false);
        ts2.SetActive(false);
        ts3.SetActive(false);
        ts4.SetActive(false);
        ts5.SetActive(false);
        ts6.SetActive(false);
        ts7.SetActive(false);
        ts8.SetActive(false);
        ts9.SetActive(false);
        ts10.SetActive(false);
        ts11.SetActive(false);
        ts12.SetActive(false);
        ts13.SetActive(false);
        ts14.SetActive(false); 
        ts15.SetActive(false);
        ts16.SetActive(false);
        ts17.SetActive(false);
        //chromaticab.enabled = false;
        bloom.enabled = false;
        hue.enabled = false;
        hue2.enabled = false;
        hue3.enabled = false;
        saturation.enabled = false;
        film.enabled = false;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
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
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
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
        //if (moveInput > 0)
        //{
        //    walk.Play();

        //}


        
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
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.linearVelocity = jumpDir * jumpForce;
            //isJumping = true;
            animator.SetBool("IsJumping", true);
            AudioSource.PlayOneShot(jump);
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
            //this.facingRight = !this.facingRight;
        }
        else
        {
            velocity = moveInput * speed;
        }

        rb.linearVelocity = new Vector2(velocity, rb.linearVelocity.y);
    }

    //void Soundeffects(Rigidbody2D rb)
    //{
    //    if (rb.isJumping = true)
    //    {
    //        jump.Play();

    //    }

    //}


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
            this.isRevesed = true; //= !this.isRevesed;
            saturation.enabled = true; 
            //this.facingRight = !this.facingRight;
            //saturation.enabled = true;
            other.gameObject.SetActive(false);
            StartCoroutine(ResetCommands(other));
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
        if (other.gameObject.CompareTag("Danger4"))
        {
            ts4.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger5"))
        {
            ts5.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger6"))
        {
            ts6.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger7"))
        {
            ts7.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger8"))
        {
            ts8.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger9"))
        {
            ts9.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger10"))
        {
            ts10.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger11"))
        {
            ts11.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger12"))
        {
            ts12.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger13"))
        {
            ts13.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger14"))
        {
            ts14.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger15"))
        {
            ts15.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger16"))
        {
            ts16.SetActive(true);
        }
        if (other.gameObject.CompareTag("Danger17"))
        {
            ts17.SetActive(true);
        }

        if (other.gameObject.CompareTag("SpaceMonkey"))
        {
            
            rb.gravityScale = 0.5f;
            bloom.enabled = true;
            other.gameObject.SetActive(false);
            pill.Play();
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
            film.enabled = true;
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

    private IEnumerator ResetCommands(Collider2D other)
    {
        yield return new WaitForSeconds(10);
        saturation.enabled = false;
        this.isRevesed = false;
        //this.facingRight = true;
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
            Debug.Log("trigger exit");
            ts2.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger3"))
        {
            ts3.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger4"))
        {
            ts4.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger5"))
        {
            ts5.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger6"))
        {
            ts6.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger7"))
        {
            ts7.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger8"))
        {
            ts8.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger9"))
        {
            ts9.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger10"))
        {
            ts10.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger11"))
        {
            ts11.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger12"))
        {
            ts12.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger13"))
        {
            ts13.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger14"))
        {
            ts14.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger15"))
        {
            ts15.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger16"))
        {
            ts16.SetActive(false);
        }
        if (other.gameObject.CompareTag("Danger17"))
        {
            ts17.SetActive(false);
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
