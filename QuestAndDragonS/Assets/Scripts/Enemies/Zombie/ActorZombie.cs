using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class ActorZombie : MonoBehaviour , IDamagable
{
    [Header("Refernces")]
    public StateMachine stateMachine = new StateMachine();
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;

    [Header("Detection")]
    public LayerMask playerMask;
    public float chaseRange;
    public float attackRange;
    public bool inChaseRange;
    public bool inAttackRange;

    [Header("Health")]
    public UnityEvent<float, float> onDamage;
    public float damage = 1f;
    [SerializeField] private float health = 3f;
    private float currentHealth;


    public float HitPoints
    {
        get => health;
        set => currentHealth = value;
    }

    private void Awake()
    {
        currentHealth = health;
        onDamage.Invoke(currentHealth, health);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        currentHealth -= amount;
        onDamage.Invoke(currentHealth, health);
        if (currentHealth <= 0)
        {
            stateMachine.ChangeState(new DeathState(this));
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
