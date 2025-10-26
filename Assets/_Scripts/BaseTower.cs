using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BaseTower : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] protected GameObject bulletPrefab;
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
    {
        foreach (GameObject tip in cannonTips)
        {
            GameObject bullet = Instantiate(bulletPrefab,
                                    tip.transform.position,
                                    Quaternion.identity);

            bullet.GetComponent<Bullet>().endPosition = enemy.transform.position;
        }
    }


}
