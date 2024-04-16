using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private ActorZombie actor;
    private float _timer = 3f;
    private float _currentTime;
    private int _isIdleHash;


    public IdleState(ActorZombie actor) {
        this.actor = actor;
        _isIdleHash = Animator.StringToHash("IsIdle");
    }

    //Plays logic entering this state after exiting the last
    public void Enter()
    {
        _currentTime = _timer;
        actor.animator.SetBool(_isIdleHash, true);
    }

    public void Execute()
    {
        //# Timer to Switch to patrol*
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0f)
        {
            ToNextState(new PatrolState(actor));
        }

        if (actor.inChaseRange && !actor.inAttackRange) ToNextState(new ChaseState(actor));
        if (actor.inChaseRange && actor.inAttackRange) ToNextState(new AttackState(actor));
    }

    public void Exit()
    {
        actor.animator.SetBool(_isIdleHash, false);
    }

    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }
}
