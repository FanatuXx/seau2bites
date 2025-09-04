using UnityEngine;

public class Rectanglemortel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rectanglemortel"))
        {
            Debug.Log("GROSLOSER") ;
        }
    }
}
