using UnityEngine;

public class CannonballProjectile : Projectile
{
    private Vector3 destination = new Vector3(0, 0, 0);

    protected override void Update()
    {
        if (target != null)
        {
            if(destination == new Vector3(0, 0, 0))
            {
                destination = target.position;
            }
        }
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
