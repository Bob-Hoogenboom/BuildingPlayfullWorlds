using System.Collections;
using UnityEngine;

public class AttackState : IState
{
    ActorZombie actor;

    private float _attackTimer = 3f;
    private float _currentTime = 3f;
    private bool _hasAttacked = true;
    private int _attackingHash;
    private int _attackWaitHash;


    public AttackState(ActorZombie actor)
    {
        this.actor = actor;
        _attackingHash = Animator.StringToHash("Attacking");
        _attackWaitHash = Animator.StringToHash("AttackingWait");
    }

    public void Enter()
    {
        actor.agent.SetDestination(actor.transform.position);
        _currentTime = _attackTimer /2;
        actor.animator.SetBool(_attackWaitHash, true);
    }

    public void Execute()
    {
        actor.transform.LookAt(actor.player);
        if (!_hasAttacked)
        {
            //Attack Logic
            IDamagable iDamagable = actor.player.gameObject.GetComponent<IDamagable>();
            if (iDamagable == null) return;

            actor.animator.SetTrigger(_attackingHash);
            iDamagable.Damage(actor.damage);

            _hasAttacked = true;
        }

        //attack Delay timer
        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0f) { ResetAttack(); }
        }


        if (!actor.inChaseRange && !actor.inAttackRange) ToNextState(new IdleState(actor));
        if (actor.inChaseRange && !actor.inAttackRange) ToNextState(new ChaseState(actor));
    }

    public void Exit()
    {
        actor.animator.SetBool(_attackWaitHash, false);
    }

    public void ToNextState(IState newState)
    {
        actor.stateMachine.ChangeState(newState);
    }

    private void ResetAttack()
    {
        actor.animator.ResetTrigger(_attackingHash);
        _hasAttacked = false;
        _currentTime = _attackTimer;
    }
}
