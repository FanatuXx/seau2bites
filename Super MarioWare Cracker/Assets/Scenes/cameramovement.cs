using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float duration = 2f;
    private float elapsedTime = 0f;
    private Vector3 startPosition;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            isMoving = true;
            elapsedTime = 0f;
            startPosition = transform.position;
        }

        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, target.position, t);

            if (t >= 1)
            {
                isMoving = false;
            }
        }
    }
}
