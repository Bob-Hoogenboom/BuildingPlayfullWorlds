public class ChaseState : IState
{
    private ActorZombie actor;

    //Constructor
    public ChaseState(ActorZombie actor)
    {
        this.actor = actor;
    }

    //Plays logic entering this state after exiting the last
    public void Enter()
    {
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
    }

    //Handles the transition to the next state
    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }
}
