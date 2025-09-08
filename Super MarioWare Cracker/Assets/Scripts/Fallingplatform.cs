using System.Collections;
using UnityEngine;

public class Fallingplatform : MonoBehaviour
{
  

    public float fallDelay = 2f;
    public float destroyWait = 1f;

    bool isFalling;
    Rigidbody2D rb;
    private void Start()
   {
            
            rb = GetComponent<Rigidbody2D>();
        }
   
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlatformDrop());
        }

    }

    IEnumerator PlatformDrop ()
    {
        Debug.Log("dropthemic");
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyWait);
       
    }
}
