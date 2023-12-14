using UnityEngine;
using UnityEngine.AI;

public class EZPatrol : State
{
    public EZPatrol(ZombieBehaviour zombieSm) : base("Patrol", zombieSm) { }

    private bool _walkPointSet;
    public Vector3 walkPoint;
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
        //checks if a walkpoint is assigned
        if (!_walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            ((ZombieBehaviour) fsm).navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = ((ZombieBehaviour) fsm).transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
            fsm.SwitchState(((ZombieBehaviour)fsm).idleState);
        }
        
        //if the player gets too close the zombie switches to chase state
        if (((ZombieBehaviour)fsm).playerInDetectRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).chaseState);
        }
    }

    //assigns a random walkable walkpoint in range of the zombie
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-((ZombieBehaviour) fsm).walkPointRange, ((ZombieBehaviour) fsm).walkPointRange);
        float randomX = Random.Range(-((ZombieBehaviour) fsm).walkPointRange, ((ZombieBehaviour) fsm).walkPointRange);
        
        Vector3 zombiePos = ((ZombieBehaviour) fsm).transform.position;
        walkPoint = new Vector3(zombiePos.x + randomX, zombiePos.y, zombiePos.z + randomZ);

        if (Physics.Raycast(walkPoint, -((ZombieBehaviour) fsm).transform.up, 5f, ((ZombieBehaviour) fsm).groundMask))
        {
            _walkPointSet = true;
        }
    }
}
