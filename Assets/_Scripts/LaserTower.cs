using System.Collections;
using UnityEngine;

public class LaserTower : BaseTower, IAttacker
{
    [SerializeField] private int laserPower;
    [SerializeField] private float damageInterval = 0.5f;

    private LineRenderer lineRenderer;
    private Coroutine damageCoroutine;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    protected override void Update()
    {
        base.Update();
        if (enemy != null)
        {
            Shoot();
        }
        else
        {
            lineRenderer.positionCount = 0;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    void OnDestroy()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    public override void Shoot()
    {
        Vector3 cannonTipPosition = cannonTips[0].transform.position;
        Vector3 enemyPosition = enemy.transform.position;
        enemyPosition.y += 1;

        Vector3[] points = { cannonTipPosition, enemyPosition };

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

        if (damageCoroutine == null && enemy != null)
        {
            damageCoroutine = StartCoroutine(DamageEnemyCoroutine());
        }
    }

    private IEnumerator DamageEnemyCoroutine()
    {
        while (enemy != null)
        {
            DealDamage(laserPower);
            yield return new WaitForSeconds(damageInterval);
        }

        damageCoroutine = null;
    }

    public void DealDamage(int damage = 0)
    {
        if (enemy != null)
        {
            enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
        }
    }
}