using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesPrefabs;
    [SerializeField] private int wave;
    [SerializeField] private List<int> enemiesPerWave;

    private int enemiesCountDuringCurrentWave;

    public delegate void WaveFinished();
    public event WaveFinished OnWaveFinished;

    void Start()
    {
        wave = 0;
        ConfigureEnemiesCount();
        InstantiateEnemy();
    }

    public void FinishWave()
    {
        if (OnWaveFinished != null)
        {
            OnWaveFinished();
        }
    }

    public void ConfigureEnemiesCount()
    {
        enemiesCountDuringCurrentWave = enemiesPerWave[wave];
    }

    public void InstantiateEnemy()
    {
        int randomIndex = Random.Range(0, enemiesPrefabs.Count);

        GameObject enemy = Instantiate(enemiesPrefabs[randomIndex], transform.position, Quaternion.identity);
        enemiesCountDuringCurrentWave--;

        if (enemiesCountDuringCurrentWave < 0)
        {
            wave++;
            ConfigureEnemiesCount();
            FinishWave();
            return;
        }

        Invoke("InstantiateEnemy", 2);
    }


}
