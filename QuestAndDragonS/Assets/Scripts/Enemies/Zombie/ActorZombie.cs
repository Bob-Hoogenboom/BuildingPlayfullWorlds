using UnityEngine;
using UnityEngine.AI;

public class ActorZombie : MonoBehaviour
{
    [Header("Refernces")]
    public StateMachine stateMachine = new StateMachine();
    public NavMeshAgent agent;
    public Transform player;

    [Header("Detection")]
    public LayerMask groundMask;
    public LayerMask playerMask;
    public float chaseRange;
    public float attackRange;
    public bool inChaseRange;
    public bool inAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        stateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        stateMachine.Update();
        PlayerInRange();
    }

    private void PlayerInRange()
    {
        inChaseRange = Physics.CheckSphere(transform.position, chaseRange, playerMask);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
