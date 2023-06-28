using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpawndModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpawndModes spawnMode = SpawndModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;


    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;

    private ObjectPooler _pooler;

    private WayPoint _waypoint;
    // Start is called before the first frame update
    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _waypoint = GetComponent<WayPoint>();

        _enemiesRemaining = enemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        EnemyManager enemy = newInstance.GetComponent<EnemyManager>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();
        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawndModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = getRandomDelay();
        }
        return delay;
    }

    private float getRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }


    
    
}

