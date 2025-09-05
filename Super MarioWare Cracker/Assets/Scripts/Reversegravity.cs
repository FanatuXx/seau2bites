using UnityEngine;

public class Reversegravity : MonoBehaviour
{
    public float m_duration = 5f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Reversegravity"))
        {

            rb.gravityScale *= -1;
         
            other.gameObject.SetActive(false);

           
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.y *= -1;
                gameObject.transform.localScale = currentScale;

            
        }

 
    }
  
}
