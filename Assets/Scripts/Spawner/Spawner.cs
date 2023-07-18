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
    [SerializeField] private Canvas winPanel;
    [Header("Settings")]
    [SerializeField] private SpawndModes spawnMode = SpawndModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private int waveCount = 3;
    [SerializeField] private float delayBtwWaves = 1f;
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;


    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")]
    [SerializeField] private ObjectPooler enemyWave5Pooler;
    [SerializeField] private ObjectPooler enemyWave6To10Pooler;
    [SerializeField] private ObjectPooler enemyWave11To15Pooler;
    [SerializeField] private ObjectPooler enemyWave16To20Pooler;
    [SerializeField] private ObjectPooler enemyWave21To25Pooler;
    [SerializeField] private ObjectPooler enemyWave26To30Pooler;


    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;
    private int _wavesRemainning;

    public int CurrentWave { get; set; }

    private WayPoint _waypoint;
    // Start is called before the first frame update
    void Start()
    {
        _waypoint = GetComponent<WayPoint>();
        _wavesRemainning = waveCount;
        _enemiesRemaining = enemyCount;
        CurrentWave = 1;
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
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        EnemyManager enemy = newInstance.GetComponent<EnemyManager>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();
        enemy.transform.localPosition = _waypoint.GetWaypointPosition(0);
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

    private ObjectPooler GetPooler()
    {
        int currentWave = CurrentWave;
        if (currentWave <= 5) 
        {
            return enemyWave5Pooler;
        }

        if (currentWave > 5 && currentWave <= 10) 
        {
            return enemyWave6To10Pooler;
        }
        
        if (currentWave > 10 && currentWave <= 15) 
        {
            return enemyWave11To15Pooler;
        }
        
        if (currentWave > 15 && currentWave <= 20) 
        {
            return enemyWave16To20Pooler;
        }
        
        if (currentWave > 20 && currentWave <= 25) 
        {
            return enemyWave21To25Pooler;
        }

        if (currentWave > 25 && currentWave <= 30)
        {
            return enemyWave26To30Pooler;
        }

        return null;
    }

    private float getRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRemaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }
    private void RecordEndmy(EnemyManager enemy)
    {
        _enemiesRemaining--;
        if (_enemiesRemaining == 0)
        {
            _wavesRemainning--;
            CurrentWave++;

            StartCoroutine(NextWave());

        }
        if (_wavesRemainning == 0 && _enemiesRemaining == 0)
        {
            winPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnEnable()
    {
        EnemyManager.OnEndReached += RecordEndmy;
        EnemyHealth.OnEnemyKilled += RecordEndmy;
    }
    private void OnDisable()
    {
        EnemyManager.OnEndReached -= RecordEndmy;
        EnemyHealth.OnEnemyKilled -= RecordEndmy;
    }



}

