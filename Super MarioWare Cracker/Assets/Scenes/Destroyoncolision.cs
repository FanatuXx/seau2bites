using UnityEngine;

public class Destroyoncollision2D : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }

}
