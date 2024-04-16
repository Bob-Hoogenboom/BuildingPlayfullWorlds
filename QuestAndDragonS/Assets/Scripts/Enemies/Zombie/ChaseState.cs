using UnityEngine;

public class ChaseState : IState
{
    private ActorZombie actor;
    private int _isChasingHash;

    public ChaseState(ActorZombie actor)
    {
        this.actor = actor;
        _isChasingHash = Animator.StringToHash("IsChasing");
    }

    //Plays logic entering this state after exiting the last
    public void Enter()
    {
        actor.animator.SetBool(_isChasingHash, true);
    }

    //Plays logic every frame synchronized with Update()
    public void Execute()
    {
        actor.agent.SetDestination(actor.player.position);

        if (!actor.inChaseRange && !actor.inAttackRange) ToNextState(new IdleState(actor));
        if (actor.inChaseRange && actor.inAttackRange) ToNextState(new AttackState(actor));
    }

    //Plays logic when exiting to a next state
    public void Exit()
    {
        actor.animator.SetBool(_isChasingHash, false);
    }

    //Handles the transition to the next state
    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }
}
