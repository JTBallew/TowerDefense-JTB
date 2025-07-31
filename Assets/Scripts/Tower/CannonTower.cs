using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonballPrefab;
    private Vector3 endPoint = new Vector3 (10, 0, 10);

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
        float furthestFromEnd = float.MinValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnd = Vector3.Distance(endPoint, enemy.transform.position);
            if (distanceToEnd > furthestFromEnd)
            {
                furthestFromEnd = distanceToEnd;
                lastEnemy = enemy;
            }
        }
        return lastEnemy;
    }
}
