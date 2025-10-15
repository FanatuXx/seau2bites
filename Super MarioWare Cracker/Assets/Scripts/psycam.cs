using UnityEngine;

public class psycam : MonoBehaviour
{
    Vector3 startPosition = new Vector3(1.8f, 2.7f, 0);
    Vector3 targetPosition = new Vector3(1.8f, -145f, 0);
    float initialMoveSpeed = 1f;
    float finalMoveSpeed = 2f;
    float currentMoveSpeed;

    float totalDistance;
    bool hasStarted = false;

    void Start()
    {
        totalDistance = Vector3.Distance(startPosition, targetPosition);
        currentMoveSpeed = initialMoveSpeed;

        // Mets la cam�ra � la position de d�part
        if (!hasStarted)
        {
            transform.position = startPosition;
            hasStarted = true;
        }
    }

    void Update()
    {
        // Calcule la distance d�j� parcourue
        float distanceCovered = Vector3.Distance(startPosition, transform.position);

        // Calcule de la progression (0 � 1)
        float progress = Mathf.Clamp01(distanceCovered / totalDistance);

        // Acc�l�re la vitesse en fonction de la progression
        currentMoveSpeed = Mathf.Lerp(initialMoveSpeed, finalMoveSpeed, progress);

        // D�place la cam�ra vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentMoveSpeed * Time.deltaTime);
    }
}
