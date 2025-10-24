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
    [SerializeField] private SelectedTower selectedTower;
    [SerializeField] private List<GameObject> towersPrefabs;

    void OnEnable()
    {
        touchController.OnPlatformTouched += CreateTower;
    }

    void OnDisable()
    {
        touchController.OnPlatformTouched -= CreateTower;
    }

    private void CreateTower(GameObject platform)
    {
        if (platform.transform.childCount == 0)
        {
            Debug.Log("Creating Tower");
            int towerIndex = (int)selectedTower;
            Vector3 positionToInstantiate = platform.transform.position;
            positionToInstantiate.y += 0.5f;
            GameObject instantiatedTower = Instantiate<GameObject>(towersPrefabs[towerIndex], positionToInstantiate, Quaternion.identity);
            instantiatedTower.transform.SetParent(platform.transform);
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
}
