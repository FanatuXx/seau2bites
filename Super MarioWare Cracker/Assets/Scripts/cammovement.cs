using UnityEngine;

public class cammovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Example using MoveTowards in Unity
    Vector3 startPosition = new Vector3(1.8f, 0, 0);
    Vector3 targetPosition = new Vector3(1.8f, -142f, 0);
    float moveSpeed = 1f;

    void Update()
    {
        // Move the object's position towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}