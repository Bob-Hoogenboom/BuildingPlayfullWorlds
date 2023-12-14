using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : FSM
{

    public EZIdle idleState;
    public EZPatrol patrolState;
    public EZChase chaseState;
    public EZAttack attackState;
    public EZDeath deathState;

    [Header("Detection")] 
    public LayerMask groundMask;
    public LayerMask playerMask;
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    
    //Patrol Variables:
    public float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks;


    public bool playerInDetectRange, playerInAttackRange;

    public NavMeshAgent navAgent;
    public Transform player;

    private Vector3 detectSpherePos;
    private Vector3 attackSpherePos;

    private void Awake()
    {
        idleState = new EZIdle(this);
        patrolState = new EZPatrol(this);
        chaseState = new EZChase(this);
        attackState = new EZAttack(this);
        deathState = new EZDeath(this);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
