using UnityEngine;

public class TroncheSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] Tronches;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    private float _timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)

        {
            int randEnemy = Random.Range(0, Tronches.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(Tronches[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);

            SetTimeUntilSpawn();
        }
    }
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
