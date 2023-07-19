using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public enum SpawndModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    public static Action OnWaveCompleted;

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
    [SerializeField] private ObjectPooler enemyWave3Pooler;
    [SerializeField] private ObjectPooler enemyWave4To6Pooler;
    [SerializeField] private ObjectPooler enemyWave7To9Pooler;
    [SerializeField] private ObjectPooler enemyWave10To12Pooler;
    [SerializeField] private ObjectPooler enemyWave13To15Pooler;



    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;
    private int _wavesRemainning;

    public int CurrentWave { get; set; }

    private WayPoint _waypoint;
    // Start is called before the first frame update
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        switch (difficulty)
        {
            case 0: // Easy
                waveCount = 9;
                delayBtwWaves = 4;
                enemyCount = 10;
                minRandomDelay = 0.5f;
                maxRandomDelay = 2;
                break;
            case 1: // Medium
                waveCount = 9;
                delayBtwWaves = 3;
                enemyCount = 15;
                minRandomDelay = 0.5f;
                maxRandomDelay = 2;
                break;
            case 2: // Hard
                waveCount = 15;
                delayBtwWaves = 3;
                enemyCount = 15;
                minRandomDelay = 0.5f;
                maxRandomDelay = 1.5f;
                break;
        }

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
        if (currentWave <= 3) 
        {
            return enemyWave3Pooler;
        }

        if (currentWave > 3 && currentWave <= 6) 
        {
            return enemyWave4To6Pooler;
        }
        
        if (currentWave > 6 && currentWave <= 9) 
        {
            return enemyWave7To9Pooler;
        }
        
        if (currentWave > 9 && currentWave <= 12) 
        {
            return enemyWave10To12Pooler;
        }
        
        if (currentWave > 12 && currentWave <= 15) 
        {
            return enemyWave13To15Pooler;
        }

        return null;
    }

    private float getRandomDelay()
    {
        float randomTimer = UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRemaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }
    private async void RecordEndmy(EnemyManager enemy)
    {
        _enemiesRemaining--;
        if (_enemiesRemaining == 0 && _wavesRemainning > 0)
        {
            _wavesRemainning--;
            CurrentWave++;
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());

        }
        if (_wavesRemainning == 0 && _enemiesRemaining == 0)
        {
            await Task.Delay((int)delayBtwWaves*1000); 
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

