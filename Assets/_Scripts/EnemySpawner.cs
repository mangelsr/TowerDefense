using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesPrefabs;
    [SerializeField] public int wave { get; private set; }
    [SerializeField] private List<int> enemiesPerWave;

    [SerializeField] private int enemiesCountDuringCurrentWave;
    public bool waveStarted { get; private set; }
    public List<GameObject> generatedEnemies { get; private set; }
    private bool hasFinishSpawning = false;

    public delegate void WaveState();
    public event WaveState OnWaveStarted;
    public event WaveState OnWaveFinished;
    public event WaveState OnWaveWon;

    void Awake()
    {
        generatedEnemies = new List<GameObject>();
    }

    void Start()
    {
        wave = 0;
    }

    void FixedUpdate()
    {
        if (waveStarted && hasFinishSpawning && generatedEnemies.Count == 0)
        {
            WinWave();
        }
    }

    private void FinishWave()
    {
        if (OnWaveFinished != null)
        {
            OnWaveFinished();
        }
    }

    private void ConfigureEnemiesCount()
    {
        enemiesCountDuringCurrentWave = enemiesPerWave[wave];
    }

    private void InstantiateEnemy()
    {
        int randomIndex = Random.Range(0, enemiesPrefabs.Count);

        GameObject enemy = Instantiate(enemiesPrefabs[randomIndex], transform.position, Quaternion.identity);
        generatedEnemies.Add(enemy);
        enemiesCountDuringCurrentWave--;

        if (enemiesCountDuringCurrentWave <= 0)
        {
            wave++;
            hasFinishSpawning = true;
            ConfigureEnemiesCount();
            FinishWave();
            return;
        }

        Invoke("InstantiateEnemy", 2);
    }

    private void WinWave()
    {
        if (waveStarted && OnWaveWon != null)
        {
            OnWaveWon();
            waveStarted = false;
        }
    }

    public void StartWave()
    {
        waveStarted = true;
        hasFinishSpawning = false;
        if (OnWaveStarted != null)
        {
            OnWaveStarted();
        }
        ConfigureEnemiesCount();
        InstantiateEnemy();
    }

    public void RemoveEnemy(GameObject gameObject)
    {
        generatedEnemies.Remove(gameObject);
    }
}