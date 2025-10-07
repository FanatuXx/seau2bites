using UnityEngine;

/// <summary>
/// Component that detects when the player enters/exits a trigger zone
/// Automatically starts/stops spawning without needing CharacterController changes
/// Replace your Danger tag triggers with this component
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SpawnerTrigger : MonoBehaviour
{
    [Header("Spawner Reference")]
    public UniversalSpriteSpawner spawner; // The spawner to control

    [Header("Trigger Settings")]
    public string playerTag = "Player"; // Tag to detect (should match your player)
    public bool startOnEnter = true; // Start spawning when player enters
    public bool stopOnExit = true; // Stop spawning when player exits

    [Header("Debug")]
    public bool debugMessages = false; // Show debug messages in console

    void Start()
    {
        // Ensure the collider is set as a trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        // Find spawner if not assigned
        if (spawner == null)
        {
            spawner = GetComponent<UniversalSpriteSpawner>();
            if (spawner == null)
            {
                spawner = GetComponentInChildren<UniversalSpriteSpawner>();
            }
        }

        // Warn if no spawner found
        if (spawner == null && debugMessages)
        {
            Debug.LogWarning($"SpawnerTrigger on {gameObject.name} has no spawner assigned!");
        }
    }

    /// <summary>
    /// Called when something enters the trigger zone
    /// Starts spawning if it's the player
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering is the player
        if (other.CompareTag(playerTag) && startOnEnter && spawner != null)
        {
            if (debugMessages)
            {
                Debug.Log($"Player entered {gameObject.name} - Starting spawner");
            }

            spawner.StartSpawning();
        }
    }

    /// <summary>
    /// Called when something exits the trigger zone
    /// Stops spawning if it's the player
    /// </summary>
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting is the player
        if (other.CompareTag(playerTag) && stopOnExit && spawner != null)
        {
            if (debugMessages)
            {
                Debug.Log($"Player exited {gameObject.name} - Stopping spawner");
            }

            spawner.StopSpawning();
        }
    }

    /// <summary>
    /// Draws the trigger area in the Scene view for easy visualization
    /// </summary>
    void OnDrawGizmos()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            // Draw trigger area in cyan
            Gizmos.color = new Color(0, 1, 1, 0.3f);
            Gizmos.matrix = transform.localToWorldMatrix;

            // Draw different shapes based on collider type
            if (col is BoxCollider2D box)
            {
                Gizmos.DrawCube(box.offset, box.size);
            }
            else if (col is CircleCollider2D circle)
            {
                Gizmos.DrawSphere(circle.offset, circle.radius);
            }
        }
    }
}
