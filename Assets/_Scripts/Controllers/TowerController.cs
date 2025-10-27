using System;
using System.Collections.Generic;
using UnityEngine;

public enum SelectedTower
{
    Tower1,
    Tower2,
    Tower3,
    Tower4,
    Tower5,
}

public class TowerController : MonoBehaviour
{

    [SerializeField] private TouchController touchController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject objective;
    [SerializeField] private SelectedTower selectedTower;
    [SerializeField] private List<GameObject> towersPrefabs;

    private List<GameObject> instantiatedTowers;

    public delegate void ObjectiveEnemyUpdated();
    public event ObjectiveEnemyUpdated OnObjectiveEnemyUpdated;


    void OnEnable()
    {
        touchController.OnPlatformTouched += CreateTower;
        enemySpawner.OnWaveStarted += UpdateObjective;
        instantiatedTowers = new List<GameObject>();
    }

    void OnDisable()
    {
        touchController.OnPlatformTouched -= CreateTower;
        enemySpawner.OnWaveStarted -= UpdateObjective;
    }

    private void CreateTower(GameObject platform)
    {
        int towerCost = selectedTower switch
        {
            SelectedTower.Tower1 => 400,
            SelectedTower.Tower2 => 600,
            SelectedTower.Tower3 => 800,
            SelectedTower.Tower4 => 750,
            SelectedTower.Tower5 => 1000,
            _ => 0,
        };
        if (platform.transform.childCount == 0 && gameManager.resources >= towerCost)
        {
            Debug.Log("Creating Tower");
            int towerIndex = (int)selectedTower;
            Vector3 positionToInstantiate = platform.transform.position;
            positionToInstantiate.y += 1f;
            GameObject newTower = Instantiate<GameObject>(towersPrefabs[towerIndex], positionToInstantiate, Quaternion.identity);
            newTower.transform.SetParent(platform.transform);
            instantiatedTowers.Add(newTower);
            gameManager.ModifyResources(-towerCost);
        }
    }

    public void SetSelectedTower(int tower)
    {
        if (Enum.IsDefined(typeof(SelectedTower), tower))
        {
            selectedTower = (SelectedTower)tower;
        }
        else
        {
            Debug.LogError("That tower is not defined!");
        }
    }

    private void UpdateObjective()
    {
        if (objective == null) return;

        if (enemySpawner.waveStarted)
        {
            float shortestDistance = float.MaxValue;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemySpawner.generatedEnemies)
            {
                float distance = Vector3.Distance(
                    enemy.transform.position,
                    objective.transform.position
                );
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                foreach (GameObject towerObject in instantiatedTowers)
                {
                    BaseTower tower = towerObject.GetComponent<BaseTower>();
                    tower.enemy = nearestEnemy;
                    tower.Shoot();
                }

                if (OnObjectiveEnemyUpdated != null)
                {
                    OnObjectiveEnemyUpdated();
                }
            }
        }
        Invoke("UpdateObjective", 3);
    }
}
