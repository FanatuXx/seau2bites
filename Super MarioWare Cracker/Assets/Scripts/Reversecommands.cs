using UnityEngine;

public class Reversecommands : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Reversecommands"))
        {
            other.gameObject.SetActive(false);
            moveInput = -moveInput;
            rb.linearVelocity *= -rb.linearVelocity;
            
            
        }
    }
}
