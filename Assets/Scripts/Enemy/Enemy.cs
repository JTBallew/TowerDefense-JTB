using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private string animatorPeram_isWalkingBool;
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int moneyDropped;
    private int currentHealth;
    private NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        anim.SetBool(animatorPeram_isWalkingBool, true);
    }

    public void Initialized(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachEnd(damage);
            }
        }
    }

    private void ReachEnd(int damage)
    {
        anim.SetBool(animatorPeram_isWalkingBool, false);
        GameManager.Instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            MoneyManager.Instance.GainMoney(moneyDropped);
        }
    }
}
