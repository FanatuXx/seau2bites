using UnityEngine;

public class Finish : MonoBehaviour

{
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("BIEN OUEJ");
            GameManager.instance.NextLevel();

        }
    }
}

