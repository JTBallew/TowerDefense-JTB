using UnityEngine;

public class BallistaTower : Tower
{
    [SerializeField] private GameObject arrowPrefab;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (arrowPrefab != null)
        {
            GameObject projectileInstance = Instantiate(arrowPrefab, transform.position + new Vector3(0, 1.4f, 0), Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform, damage);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
