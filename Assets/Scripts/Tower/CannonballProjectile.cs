using UnityEngine;

public class CannonballProjectile : Projectile
{
    [SerializeField] private GameObject explosionHitbox;
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
        GameObject explosion = Instantiate(explosionHitbox, new Vector3(transform.position.x, explosionHitbox.transform.position.y, transform.position.z), explosionHitbox.transform.rotation);
        explosion.GetComponent<Explosion>().explosionDamage = damage;
        Destroy(gameObject);
    }
}
