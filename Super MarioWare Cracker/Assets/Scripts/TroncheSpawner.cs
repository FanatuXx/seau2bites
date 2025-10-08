using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class TroncheSpawner : MonoBehaviour
{
    //public Transform[] spawnPoints;
    //    public GameObject[] Tronches;

    //    public Vector3 spawnPosition;

    //    [SerializeField]
    //    private float _minimumSpawnTime;

    //    [SerializeField]
    //    private float _maximumSpawnTime;

    //    private float _timeUntilSpawn;

    //    void Awake()
    //    {
    //        SetTimeUntilSpawn();
    //    }

    //    void Update()
    //    {
    //        _timeUntilSpawn -= Time.deltaTime;

    //        if (_timeUntilSpawn <= 0)

    //        {
    //            int randEnemy = Random.Range(0, Tronches.Length);
    //            //int randSpawnPoint = Random.Range(0, spawnPoints.Length);

    //            //GameObject _Instantiate = Instantiate(Tronches[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
    //            GameObject _Instantiate = Instantiate(Tronches[randEnemy], spawnPosition, Quaternion.identity);
    //            _Instantiate.GetComponent<AlarmDead>().Launch();
    //            SetTimeUntilSpawn();
    //            // StartCoroutine(DestroySpawn());
    //        }

    //        //IEnumerator DestroySpawn()
    //        //{
    //        //    yield return new WaitForSeconds(6f);
    //        //    Destroy(gameObject);
    //        //}
    //    }
    //    private void SetTimeUntilSpawn()
    //    {
    //        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    //    }
    //}

    public GameObject[] Tronches;
    //public Vector3 spawnPosition;
    public float spawnRate = 1.0f; // How often to spawn an object
    public float destroyDelay = 6.0f; // How long an object lives

    void OnEnable()
    {
        StartCoroutine(SpawnThenDestroy()); // Call SpawnThenDestroy repeatedly
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnThenDestroy()
    {
        while (true)
        {
            // Instantiate the object
            int randEnemy = Random.Range(0, Tronches.Length);
            GameObject spawnedObject = Instantiate(Tronches[randEnemy], transform.position, Quaternion.identity);
            spawnedObject.GetComponent<AlarmDead>().Launch();

            // Schedule the object for destruction
            Destroy(spawnedObject, destroyDelay);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
