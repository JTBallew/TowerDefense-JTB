using UnityEngine;

public class TurretTower : Tower
{
    [SerializeField] private GameObject bulletPrefab;

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
        float longestAlive = float.MinValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float enemyLifespan = enemy.timeAlive;
            if (enemyLifespan > longestAlive)
            {
                longestAlive = enemyLifespan;
                firstEnemy = enemy;
            }
        }
        return firstEnemy;
    }
}
