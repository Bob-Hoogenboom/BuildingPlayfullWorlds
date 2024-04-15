using UnityEngine;
using UnityEngine.AI;

public class ActorZombie : MonoBehaviour , IDamagable
{
    [Header("Refernces")]
    public StateMachine stateMachine = new StateMachine();
    public NavMeshAgent agent;
    public Transform player;

    [Header("Detection")]
    public LayerMask playerMask;
    public float chaseRange;
    public float attackRange;
    public bool inChaseRange;
    public bool inAttackRange;

    [Header("Health")]
    public float damage = 1f;

    [SerializeField] private float health = 3f;
    public float HitPoints
    {
        get => health;
        set => health = value;
    }

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

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            stateMachine.ChangeState(new IdleState(this));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
