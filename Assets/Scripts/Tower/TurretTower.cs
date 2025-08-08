using UnityEngine;

public class TurretTower : Tower
{
    [SerializeField] private GameObject bulletPrefab;
    private Vector3 endPoint = new Vector3(10, 0, 10);

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (bulletPrefab != null)
        {
            GameObject projectileInstance = Instantiate(bulletPrefab, transform.position + new Vector3(0, 1.4f, 0), Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy firstEnemy = null;
        float ClosestToEnd = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnd = Vector3.Distance(endPoint, enemy.transform.position);
            if (distanceToEnd < ClosestToEnd)
            {
                ClosestToEnd = distanceToEnd;
                firstEnemy = enemy;
            }
        }
        return firstEnemy;
    }
}
