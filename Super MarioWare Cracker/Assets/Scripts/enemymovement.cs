using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Vitesse du mouvement
    public float moveDistance = 3f; // Distance du mouvement de va-et-vient
    private float startY; // Position Y de départ


    private bool hasLineOfSight = false; //los
    public GameObject player; //los

    void Start()
    {
        startY = transform.position.y; // Enregistrer la position Y de départ
    }

    void Update()
    {
        // Calculer le mouvement en fonction du temps et de la distance
        float newY = startY + Mathf.PingPong(Time.time * speed, moveDistance);

        // Appliquer le mouvement
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
    
    
        
 
    private void FixedUpdate() //Toute la suite est LOS
    {
     RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
     Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
     if (ray.collider != null)
     {
     hasLineOfSight = ray.collider.CompareTag("Player");
     if (hasLineOfSight)
     {

      Debug.Log("CATCH");


     }
    else
    {
     Debug.Log("PAS CATCH");


    }
     }
     }
}
