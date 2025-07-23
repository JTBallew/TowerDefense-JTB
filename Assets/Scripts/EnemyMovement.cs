using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform endpoint;
    [SerializeField] private string animatorPeram_isWalkingBool;
    private NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        agent.SetDestination(endpoint.position);
        anim.SetBool(animatorPeram_isWalkingBool, true);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                anim.SetBool(animatorPeram_isWalkingBool, false);
            }
        }
    }
}
