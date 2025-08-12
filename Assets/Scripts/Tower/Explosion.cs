using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    public int explosionDamage;

    private float explosionTime = 0.1f;
    private List<Enemy> enemiesInExplosion = new List<Enemy>();

    private void Start()
    {
        Invoke("DamageEnemies", explosionTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemiesInExplosion.Add(enemy);
        }
    }

    public void DamageEnemies()
    {
        foreach (Enemy enemy in enemiesInExplosion)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }
}
