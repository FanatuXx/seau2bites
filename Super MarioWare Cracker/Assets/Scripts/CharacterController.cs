using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class CharacterController : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private InputAction move;
    public InputActionReference jump;
    bool jumpNow = false;

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
    public bool canbeKilled = true;

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
    public AudioSource needle;
    public AudioSource jumpAudiosource;
    [SerializeField] private AudioClip[] jumpAudios;
    //public AudioSource walk;

    AudioClip impact;
    public AudioSource audioSource;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInputActions = new PlayerInputActions();

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
        chromaticab.enabled = false;
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
        audioSource = GetComponent<AudioSource>();  

    }

     private void PlayJump()
     {
        if (jumpAudiosource.isPlaying) return;
        int index = Random.Range(0, jumpAudios.Length);
        jumpAudiosource.clip = jumpAudios[index];
        jumpAudiosource.Play();
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


    void Update()
    {
        //moveInput = Input.GetAxisRaw("Horizontal");
    
        Vector2 jumpDir = Vector2.up;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        UpdateHiddenStatus();


        if (move.ReadValue<Vector2>().x > 0 && !facingRight) 

        {
            Flip();
        }

        if (move.ReadValue<Vector2>().x < 0 && facingRight)
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
        if (isGrounded == true && jumpNow)//Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.linearVelocity = jumpDir * jumpForce;
            animator.SetBool("IsJumping", true);
            PlayJump();


        }
        else
        {
            jumpNow = false;
        }


            animator.SetFloat("Speed", Mathf.Abs(move.ReadValue<Vector2>().x));//moveInput)); 
    }

    private void UpdateHiddenStatus()
    {
        bool behindCachette = false;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Cachette"))
            {
                behindCachette = true;
                break;
            }
        }

        bool noFacesVisible = FaceVisibilityManager.Instance != null && !FaceVisibilityManager.Instance.AnyFaceVisible();

        bool wasHidden = isHidden;
        isHidden = behindCachette || noFacesVisible;

        if (wasHidden != isHidden)
        {
            Debug.Log($"Player hidden status changed: {isHidden} (Behind Cachette: {behindCachette}, No Faces Visible: {noFacesVisible})");
        }
    }




    void FixedUpdate()
    {
        float velocity;
       

        if (this.isRevesed)
        {
            velocity = move.ReadValue<Vector2>().x * speed * -1;//moveInput * speed * -1;
            
        }
        else
        {
            velocity = move.ReadValue<Vector2>().x * speed;// moveInput * speed;
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
          
            other.gameObject.SetActive(false);
            pill.Play();
            hue.enabled = true;
            StartCoroutine(hueshift(other));
            StartCoroutine(resetgravityhue(other));
            StartCoroutine(resethue(other));


        }

        if (other.gameObject.CompareTag("Reversecommands"))
        {
            this.isRevesed = true; 
            saturation.enabled = true; 
            other.gameObject.SetActive(false);
            needle.Play();
            StartCoroutine(ResetCommands(other));
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
            
            //enemymovement.speed = 0.5f;
         
            film.enabled = true;
            other.gameObject.SetActive(false);
            pill.Play();
            StartCoroutine(ResetEscargot(other));
            enemymovement.speed = 0.5f;

        }


        #region Spawners

        /*if (other.gameObject.CompareTag("Cachette"))
        {
            isHidden = true;
        } */

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

        #endregion
    }
    private IEnumerator ResetGrav(Collider2D other)
    {
        yield return new WaitForSeconds(5);
        rb.gravityScale = V;
        bloom.enabled = false;
        other.gameObject.SetActive(true);
    }
    private IEnumerator ResetEscargot(Collider2D other)
    {
        yield return new WaitForSeconds(10);
        //enemymovement.speed = null;
        film.enabled = false;
        other.gameObject.SetActive(true);
    }

    private IEnumerator ResetCommands(Collider2D other)
    {
        yield return new WaitForSeconds(20);
        saturation.enabled = false;
        this.isRevesed = false;
        
    }


    private IEnumerator hueshift(Collider2D other)
    {
        //StartCoroutine(resetgravityhue(other));

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

    IEnumerator resetgravityhue(Collider2D other)
    {
        yield return new WaitForSeconds(15);
       
        rb.gravityScale = V;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.y *= -1;
        gameObject.transform.localScale = currentScale;
        other.gameObject.SetActive(true);



    }

    IEnumerator resethue(Collider2D other)
    {
        yield return new WaitForSeconds(19);
        hue.enabled = false;
        hue2.enabled = false;
        hue3.enabled = false;
    }






    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.gameObject.CompareTag("Cachette"))
        {
            isHidden = false;
        } */

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
