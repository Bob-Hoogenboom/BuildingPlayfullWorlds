using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    ActorZombie actor;

    public float attackDelay;

    private bool hasAttacked;

    public AttackState(ActorZombie actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        Debug.Log("AttackState.Enter()");
    }

    public void Execute()
    {
        if (!actor.inChaseRange && !actor.inAttackRange) ToNextState(new IdleState(actor));
        if (actor.inChaseRange && !actor.inAttackRange) ToNextState(new ChaseState(actor));
    }

    public void Exit()
    {
        Debug.Log("AttackState.Exit()");
    }

    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }
}
