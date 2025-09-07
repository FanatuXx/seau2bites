using UnityEngine;

public class Victory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Victoire"))
        {
            Debug.Log("BIEN OUEJ");
            
        }
    }
}