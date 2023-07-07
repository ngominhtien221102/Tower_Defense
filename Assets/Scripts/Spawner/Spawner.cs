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

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;
    private int _wavesRemainning;

    private ObjectPooler _pooler;

    private WayPoint _waypoint;
    // Start is called before the first frame update
    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _waypoint = GetComponent<WayPoint>();
        _wavesRemainning = waveCount;
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
            if(_wavesRemainning == 0)
            {
                winPanel.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                StartCoroutine(NextWave());

            }
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

