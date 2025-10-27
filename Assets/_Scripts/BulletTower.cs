
using UnityEngine;

public class BulletTower : BaseTower
{
    [SerializeField] private GameObject bulletPrefab;

    public override void Shoot()
    {

        foreach (GameObject tip in cannonTips)
        {
            GameObject bullet = Instantiate(bulletPrefab,
                                    tip.transform.position,
                                    Quaternion.identity);

            Vector3 enemyPosition = enemy.transform.position;
            enemyPosition.y += 1;

            bullet.GetComponent<Bullet>().endPosition = enemyPosition;
        }
    }
}
