using Unity.VisualScripting;
using UnityEngine;

public class Reversegrav : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
