using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime;
    protected Transform target;

    protected void Start()
    {
        Destroy(gameObject, lifetime);
    }

    protected virtual void Update()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected abstract void OnTriggerEnter(Collider other);

    public void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }
}
