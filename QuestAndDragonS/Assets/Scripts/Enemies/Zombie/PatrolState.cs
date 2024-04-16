using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private ActorZombie actor;

    [Header("Patroling")]
    public Vector3 walkPoint;
    public float walkPointRange = 10f;
    private bool _walkPointSet;
    private int _isPatrollingHash;

    public PatrolState(ActorZombie actor)
    {
        this.actor = actor;
        _isPatrollingHash = Animator.StringToHash("IsPatrolling");
    }

    //Plays logic entering this state after exiting the last
    public void Enter()
    {
        actor.animator.SetBool(_isPatrollingHash, true);
    }

    //Plays logic every frame synchronized with Update()
    public void Execute()
    {
        if (!_walkPointSet) GetNewWalkPoint();

        if (_walkPointSet)
        {
            actor.agent.SetDestination(walkPoint);
        }

        Vector3 disToWalkPoint = actor.transform.position - walkPoint;

        if(disToWalkPoint.magnitude <1)
        {
            ToNextState(new IdleState(actor));
        }

        if (actor.inChaseRange && !actor.inAttackRange) ToNextState(new ChaseState(actor));
        if (actor.inChaseRange && actor.inAttackRange) ToNextState(new AttackState(actor));
    }

    //Plays logic when exiting to a next state
    public void Exit()
    {
        _walkPointSet = false;
        actor.animator.SetBool(_isPatrollingHash, false);
    }

    //Handles the transition to the next state
    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }

    //Searches for a new walkpoint when no walkpoint is assigned yet
    private void GetNewWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(actor.transform.position.x + randomX, actor.transform.position.y, actor.transform.position.z + randomZ);

        //checks if the walkpoint is on the NavMesh 
        if (NavMesh.SamplePosition(walkPoint, out _, 1.0f, NavMesh.AllAreas)) _walkPointSet = true;
    }
}
