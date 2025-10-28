using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [HideInInspector] public GameObject enemy;
    [SerializeField] protected List<GameObject> cannonTips;
    [SerializeField] protected Transform rotatingPart;

    protected virtual void Update()
    {
        if (enemy != null) Aim();
    }

    private void Aim()
    {
        if (rotatingPart != null)
            rotatingPart.LookAt(enemy.transform);
        else
            transform.LookAt(enemy.transform);
    }

    public virtual void Shoot()
    { }
}
