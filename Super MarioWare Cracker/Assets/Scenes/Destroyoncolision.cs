using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            Destroy(other.gameObject);
        }
    }
}
