using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : FSM
{
    [HideInInspector]
    public EZIdle idleState;
    public EZPatrol patrolState;
    public EZChase chaseState;
    public EZAttack attackState;
    public EZDeath deathState;

    [Header("Detection")] 
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    
    public bool playerInDetectRange, playerInAttackRange;
    
    private NavMeshAgent navAgent;
    private Transform player;

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
        Vector3 pos = transform.position;
        detectSpherePos = new Vector3(pos.x, pos.y, pos.z + detectRange / 2);
        attackSpherePos = new Vector3(pos.x, pos.y, pos.z + attackRange / 2);

        playerInDetectRange = Physics.CheckSphere(detectSpherePos, detectRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(attackSpherePos, attackRange, playerMask);
    }

    protected override State GetInitialState()
    {
        return idleState;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        detectSpherePos = new Vector3(pos.x, pos.y, pos.z + detectRange / 2);
        attackSpherePos = new Vector3(pos.x, pos.y, pos.z + attackRange / 2);
        
        Gizmos.DrawWireSphere(detectSpherePos, detectRange);
        Gizmos.DrawWireSphere(attackSpherePos, attackRange);
    }
}
