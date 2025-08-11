using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonballPrefab;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        if (cannonballPrefab != null)
        {
            GameObject projectileInstance = Instantiate(cannonballPrefab, transform.position + new Vector3(0, 1.4f, 0), Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();

        Enemy lastEnemy = null;
        float shortestAlive = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float enemyLifespan = enemy.timeAlive;
            if (enemyLifespan < shortestAlive)
            {
                shortestAlive = enemyLifespan;
                lastEnemy = enemy;
            }
        }
        return lastEnemy;
    }
}
