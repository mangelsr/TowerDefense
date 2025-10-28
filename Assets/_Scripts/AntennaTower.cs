using System.Collections.Generic;
using UnityEngine;

public class AntennaTower : BaseTower, IAttacker
{
    [SerializeField] private float lightingDivisions = 10;
    [SerializeField] private int lightingPower;
    private LineRenderer lineRenderer;
    private List<Vector3> points;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if (enemy != null)
        {
            Shoot();
            DealDamage(lightingPower);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    public override void Shoot()
    {
        Vector3 cannonTipPosition = cannonTips[0].transform.position;
        Vector3 enemyPosition = enemy.transform.position;

        points = GetPoints();
        points.Insert(0, cannonTipPosition);

        enemyPosition.y += 1;

        points.Add(enemyPosition);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    private List<Vector3> GetPoints()
    {
        List<Vector3> temporalPoints = new List<Vector3>();
        float divider = 1f / lightingDivisions;
        float linear = 0f;
        bool isPositive = false;

        if (lightingDivisions == 0)
        {
            Debug.LogError("Invalid value for property `lightingDivisions`");
            return null;
        }

        Vector3 cannonTipPosition = cannonTips[0].transform.position;
        Vector3 enemyPosition = enemy.transform.position;

        if (lightingDivisions == 1)
        {
            Vector3 point = Vector3.Lerp(cannonTipPosition, enemyPosition, 0.5f);
            temporalPoints.Add(point);
            return temporalPoints;
        }

        for (int i = 0; i < lightingDivisions; i++)
        {
            if (i == 0) linear = divider / 2;
            else linear += divider;

            Vector3 point = Vector3.Lerp(cannonTipPosition, enemyPosition, linear);

            if (isPositive) point.x += Random.value * 2;
            else point.x += Random.value * 2;

            isPositive = !isPositive;

            temporalPoints.Add(point);
        }

        return temporalPoints;
    }


    public void DealDamage(int damage = 0)
    {
        enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
    }

}
