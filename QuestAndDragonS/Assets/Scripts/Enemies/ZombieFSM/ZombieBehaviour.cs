using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ZombieBehaviour : FSM, IDamageable, IStunned
{
    [HideInInspector]
    public EZIdle idleState;
    public EZPatrol patrolState;
    public EZChase chaseState;
    public EZAttack attackState;
    public EZDeath deathState;
    public EZStun stunState;

    [Header("Detection")]
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;

    public LayerMask groundMask;
    public LayerMask playerMask;

    public NavMeshAgent navAgent;
    public Transform player;

    public bool playerInDetectRange, playerInAttackRange;

    //private Vector3 detectSpherePos;
    //private Vector3 attackSpherePos;

    [Header("Patrol")]
    public float walkPointRange;

    [Header("Attack")]
    [Tooltip("Seconds between every attack")]
    public float timeBetweenAttacks = 3f;

    [Header("Stun")]
    public UnityEvent OnStunned;

    [Header("Health")]
    private bool _isDeath = false;
    public UnityEvent OnDeath;

    public float health = 5;
    public float damage = 1;

    private void Awake()
    {
        idleState = new EZIdle(this);
        patrolState = new EZPatrol(this);
        chaseState = new EZChase(this);
        attackState = new EZAttack(this);
        deathState = new EZDeath(this);
        stunState = new EZStun(this);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        base.Update();
        Vector3 pos = transform.position;

        playerInDetectRange = Physics.CheckSphere(pos, detectRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(pos, attackRange, playerMask);
    }

    protected override State GetInitialState()
    {
        return idleState;
    }

    public void Damage(float amount)
    {
        health -= amount;

        if (health <= 0 && !_isDeath)
        {
            _isDeath = true;
            SwitchState(deathState);
        }
    }

    public void Stunned()
    {
        //DRY?
        if (health <= 0 && !_isDeath)
        {
            _isDeath = true;
            SwitchState(deathState);
        }
        else
        {
            SwitchState(stunState);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
