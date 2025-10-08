using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Component that handles individual sprite movement, animation, and lifecycle
/// Automatically added to spawned sprites to control their behavior
/// </summary>
public class SpriteBehavior : MonoBehaviour
{
    // Movement and timing settings (set by spawner)
    private Vector2 moveDirection;
    private float moveSpeed;
    private float moveDuration;
    private bool useAnimation;
    private AnimationCurve movementCurve;
    private Action onDestroyed; // Callback to notify spawner when this sprite is destroyed

    // Component references
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Movement tracking
    private Vector3 startPosition;
    private float elapsedTime = 0f;

    /// <summary>
    /// Initializes the sprite with its movement and animation settings
    /// Called by the spawner after instantiation
    /// </summary>
    public void Initialize(Vector2 direction, float speed, float duration, bool enableAnimation, AnimationCurve curve, Action destroyCallback)
    {
        // Store all the movement settings
        moveDirection = direction.normalized; // Ensure direction is normalized
        moveSpeed = speed;
        moveDuration = duration;
        useAnimation = enableAnimation;
        movementCurve = curve;
        onDestroyed = destroyCallback;

        // Cache starting position for movement calculations
        startPosition = transform.position;

        // Get component references
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Enable animation if requested and animator exists
        if (useAnimation && animator != null)
        {
            animator.enabled = true;
        }

        // Start the movement and lifecycle coroutine
        StartCoroutine(MoveAndDestroy());
    }

    /// <summary>
    /// Coroutine that handles sprite movement over time and destruction
    /// Uses the movement curve to create smooth, customizable motion
    /// </summary>
    IEnumerator MoveAndDestroy()
    {
        // Move the sprite until duration is reached
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate how far through the movement we are (0 to 1)
            float normalizedTime = elapsedTime / moveDuration;

            // Use the curve to modify movement (allows for acceleration, deceleration, etc.)
            float curveValue = movementCurve.Evaluate(normalizedTime);

            // Calculate new position based on direction, speed, and curve
            Vector3 currentOffset = moveDirection * moveSpeed * curveValue * elapsedTime;
            transform.position = startPosition + currentOffset;

            // Wait for next frame
            yield return null;
        }

        // Notify the spawner that this sprite is being destroyed
        onDestroyed?.Invoke();

        // Destroy this sprite
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles sprite flipping based on movement direction
    /// Called every frame to ensure sprite faces the correct direction
    /// </summary>
    void Update()
    {
        // Flip sprite based on horizontal movement direction
        if (spriteRenderer != null)
        {
            if (moveDirection.x < 0)
            {
                spriteRenderer.flipX = true; // Moving left
            }
            else if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = false; // Moving right
            }
            // Don't change flip if moving purely vertically
        }
    }
}
