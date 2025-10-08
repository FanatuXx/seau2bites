using System.Collections;
using UnityEngine;

/// <summary>
/// Data structure to define each type of sprite that can be spawned
/// Contains all the settings for movement, animation, and spawn probability
/// </summary>
[System.Serializable]
public class SpawnableSprite
{
    [Header("Prefab Settings")]
    public GameObject prefab; // The sprite prefab to spawn

    [Header("Movement Settings")]
    public float moveDuration = 3f; // How long the sprite moves before disappearing
    public Vector2 moveDirection = Vector2.right; // Direction the sprite moves (normalized automatically)
    public float moveSpeed = 2f; // Speed of movement
    public AnimationCurve movementCurve = AnimationCurve.Linear(0, 0, 1, 1); // Movement acceleration curve

    [Header("Animation Settings")]
    public bool useAnimation = true; // Whether to enable the animator on this sprite

    [Header("Spawn Probability")]
    [Range(0f, 1f)]
    public float spawnWeight = 1f; // Higher values = more likely to be selected for spawning
}

/// <summary>
/// Universal spawner that can spawn different types of sprites with individual movement patterns
/// Replaces the need for multiple separate spawner GameObjects
/// </summary>
public class UniversalSpriteSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public SpawnableSprite[] spawnableSprites; // Array of all sprites this spawner can create
    public float spawnRate = 1f; // Time between spawns in seconds
    public float spawnDelay = 1f; // Initial delay before first spawn
    public bool autoStart = false; // Whether to start spawning automatically
    public int maxActiveSprites = 10; // Maximum number of sprites active at once

    [Header("Spawn Area")]
    public Transform[] spawnPoints; // Multiple spawn locations (optional)
    public bool useRandomSpawnPoint = true; // Pick random spawn point or use spawner position

    [Header("Spawn Position Randomization")]
    public bool useRandomXPosition = true; // Enable random X position offset
    public float spawnRangeX = 3f; // Range for random X offset (-range to +range)
    public float spawnRangeY = 0f; // Range for random Y offset (-range to +range)

    [Header("Global Movement Override")]
    public bool useGlobalMovement = false; // Override individual sprite movement settings
    public Vector2 globalMoveDirection = Vector2.right; // Global movement direction if override enabled
    public float globalMoveSpeed = 2f; // Global movement speed if override enabled
    public float globalMoveDuration = 3f; // Global movement duration if override enabled

    // Private variables for tracking spawner state
    private int currentActiveSprites = 0; // Current number of active sprites
    private bool isSpawning = false; // Whether the spawner is currently active

    void Start()
    {
        // Start spawning automatically if enabled
        if (autoStart)
        {
            StartSpawning();
        }
    }

    /// <summary>
    /// Starts the spawning process - can be called from trigger zones or other scripts
    /// </summary>
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            // Use InvokeRepeating to call SpawnSprite repeatedly at specified intervals
            InvokeRepeating(nameof(SpawnSprite), spawnDelay, spawnRate);
        }
    }

    /// <summary>
    /// Stops the spawning process - can be called when player leaves trigger zone
    /// </summary>
    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            // Cancel the repeating spawning calls
            CancelInvoke(nameof(SpawnSprite));
        }
    }

    /// <summary>
    /// Main spawning logic - creates a sprite and sets up its behavior
    /// Called automatically by InvokeRepeating when spawning is active
    /// </summary>
    void SpawnSprite()
    {
        // Don't spawn if we've reached the maximum or have no sprites configured
        if (currentActiveSprites >= maxActiveSprites || spawnableSprites.Length == 0)
            return;

        // Select which sprite to spawn based on weights
        SpawnableSprite selectedSprite = SelectRandomSprite();
        if (selectedSprite?.prefab == null)
            return;

        // Determine where to spawn the sprite
        Vector3 spawnPosition = GetSpawnPosition();

        // Create the sprite instance
        GameObject spawnedObject = Instantiate(selectedSprite.prefab, spawnPosition, Quaternion.identity);

        // Increment active sprite counter
        currentActiveSprites++;

        // Get or add the behavior component that handles movement and lifecycle
        SpriteBehavior behavior = spawnedObject.GetComponent<SpriteBehavior>();
        if (behavior == null)
        {
            behavior = spawnedObject.AddComponent<SpriteBehavior>();
        }

        // Determine movement settings (global override or individual sprite settings)
        Vector2 moveDir = useGlobalMovement ? globalMoveDirection : selectedSprite.moveDirection;
        float moveSpd = useGlobalMovement ? globalMoveSpeed : selectedSprite.moveSpeed;
        float moveDur = useGlobalMovement ? globalMoveDuration : selectedSprite.moveDuration;

        // Initialize the sprite with its movement and animation settings
        behavior.Initialize(
            moveDir,
            moveSpd,
            moveDur,
            selectedSprite.useAnimation,
            selectedSprite.movementCurve,
            () => { currentActiveSprites--; } // Callback to decrement counter when sprite is destroyed
        );
    }

    /// <summary>
    /// Selects a random sprite from the array based on spawn weights
    /// Higher weight = more likely to be selected
    /// </summary>
    SpawnableSprite SelectRandomSprite()
    {
        // Calculate total weight of all sprites
        float totalWeight = 0f;
        foreach (var sprite in spawnableSprites)
        {
            totalWeight += sprite.spawnWeight;
        }

        // Pick a random value within the total weight range
        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        // Find which sprite corresponds to the random value
        foreach (var sprite in spawnableSprites)
        {
            currentWeight += sprite.spawnWeight;
            if (randomValue <= currentWeight)
            {
                return sprite;
            }
        }

        // Fallback to first sprite if something goes wrong
        return spawnableSprites[0];
    }

    /// <summary>
    /// Determines where to spawn the next sprite
    /// Uses random spawn point if configured, otherwise uses spawner position
    /// Adds random position offset if enabled
    /// </summary>
    Vector3 GetSpawnPosition()
    {
        Vector3 basePosition;

        if (useRandomSpawnPoint && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            basePosition = spawnPoints[randomIndex].position;
        }
        else
        {
            basePosition = transform.position;
        }

        // Add random offset if enabled
        if (useRandomXPosition || spawnRangeY > 0)
        {
            float randomX = useRandomXPosition ? Random.Range(-spawnRangeX, spawnRangeX) : 0f;
            float randomY = spawnRangeY > 0 ? Random.Range(-spawnRangeY, spawnRangeY) : 0f;
            basePosition += new Vector3(randomX, randomY, 0);
        }

        return basePosition;
    }


    /// <summary>
    /// Draws visual helpers in the Scene view to show spawn points and spawner location
    /// </summary>
    void OnDrawGizmos()
    {
        // Draw spawn points as yellow spheres
        if (spawnPoints != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var point in spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawWireSphere(point.position, 0.5f);
                }
            }
        }

        // Draw spawner position as red sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
