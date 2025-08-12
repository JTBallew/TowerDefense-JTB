using UnityEngine;

public class CatapultTower : Tower
{
    [SerializeField] private GameObject boulderPrefab;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (boulderPrefab != null)
        {
            GameObject projectileInstance = Instantiate(boulderPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform, damage);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy furthestEnemy = null;
        float furthestDistance = float.MinValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy > furthestDistance)
            {
                furthestDistance = distanceToEnemy;
                furthestEnemy = enemy;
            }
        }
        return furthestEnemy;
    }
}
