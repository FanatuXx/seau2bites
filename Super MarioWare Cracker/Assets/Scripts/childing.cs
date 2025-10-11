using UnityEngine;

public class childing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision) 
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            collision.transform.parent = transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.transform.parent = null;
        }



    }
}
