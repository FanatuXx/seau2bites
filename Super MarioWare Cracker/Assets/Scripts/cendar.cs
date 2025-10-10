using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class cendar : MonoBehaviour
{
 

    public GameObject[] Tronches;
    public float spawnRate = 1.0f; // How often to spawn an object
    public float destroyDelay = 6.0f; // How long an object lives

    void Start()
    {
        InvokeRepeating("SpawnThenDestroy", 1f, spawnRate); // Call SpawnThenDestroy repeatedly
    }

    void SpawnThenDestroy()
    {

        int randEnemy = Random.Range(0, Tronches.Length);
        GameObject spawnedObject = Instantiate(Tronches[randEnemy], transform.position, Quaternion.identity);
        Destroy(spawnedObject, destroyDelay);
       
        
    }
}
