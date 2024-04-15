public class AttackState : IState
{
    ActorZombie actor;

    private float _attackDelay = .6f;
    private bool _hasAttacked;


    public AttackState(ActorZombie actor)
    {
        this.actor = actor;
    }

    public void Enter()
    {
        actor.agent.SetDestination(actor.transform.position);
    }

    public void Execute()
    {
        actor.transform.LookAt(actor.player);
        if (!_hasAttacked)
        {
            //Attack Logic
            IDamagable iDamagable = actor.player.gameObject.GetComponent<IDamagable>();
            if (iDamagable == null) return;

            iDamagable.Damage(actor.damage);

            _hasAttacked = true;
            //# maybe replace with a timer so it doesn't need monobehaviour???
            actor.Invoke(nameof(ResetAttack), _attackDelay);
        }

        if (!actor.inChaseRange && !actor.inAttackRange) ToNextState(new IdleState(actor));
        if (actor.inChaseRange && !actor.inAttackRange) ToNextState(new ChaseState(actor));
    }

    public void Exit()
    {
    }

    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }

    private void ResetAttack()
    {
        _hasAttacked = false;
    }
}
