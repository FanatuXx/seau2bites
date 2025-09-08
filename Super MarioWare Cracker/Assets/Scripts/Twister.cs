using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector3 spawnPosition;
    public float spawnRate = 2.0f; // How often to spawn an object
    public float destroyDelay = 5.0f; // How long an object lives

    void Start()
    {
        InvokeRepeating("SpawnThenDestroy", 0f, spawnRate); // Call SpawnThenDestroy repeatedly
    }

    void SpawnThenDestroy()
    {
        // Instantiate the object
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Schedule the object for destruction
        Destroy(spawnedObject, destroyDelay);
    }
}
