using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private ActorZombie actor;
    //Constructor
    public IdleState(ActorZombie actor) {
        this.actor = actor;
    }

    public void Enter()
    {
        Debug.Log("IdleState.Enter()");
    }

    public void Execute()
    {
        if (actor.inChaseRange && !actor.inAttackRange) ToNextState(new ChaseState(actor));
        if (actor.inChaseRange && actor.inAttackRange) ToNextState(new AttackState(actor));
    }

    public void Exit()
    {
        Debug.Log("IdleState.Exit()");
        
    }

    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }
}
