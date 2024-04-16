using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    private ActorZombie actor;
    private int _dyingHash;

    public DeathState(ActorZombie actor)
    {
        this.actor = actor;
        _dyingHash = Animator.StringToHash("Dying");
    }

    //Plays logic entering this state after exiting the last
    public void Enter()
    {
        actor.agent.SetDestination(actor.transform.position);
        actor.animator.SetTrigger(_dyingHash);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }

    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }
}