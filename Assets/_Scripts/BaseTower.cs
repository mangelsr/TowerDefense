using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [HideInInspector] public GameObject enemy;
    [SerializeField] protected List<GameObject> cannonTips;

    void Update()
    {
        if (enemy != null) Aim();
    }

    private void Aim()
    {
        transform.LookAt(enemy.transform);
    }

    public virtual void Shoot()
    { }
}
