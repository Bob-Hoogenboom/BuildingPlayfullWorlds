using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : FSM
{
    [HideInInspector]
    public EZIdle idleState;
    public EZPatrol patrolState;
    public EZChase chaseState;
    public EZAttack attackState;

    //Define NavMesh/NavMeshAgent
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundMask, playerMask;
    
    private void Awake()
    {
        idleState = new EZIdle(this);
        patrolState = new EZPatrol(this);
    }

    protected override State GetInitialState()
    {
        return idleState;
    }
}
