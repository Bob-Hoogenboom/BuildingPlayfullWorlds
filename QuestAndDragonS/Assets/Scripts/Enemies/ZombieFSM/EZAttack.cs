using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EZAttack : State
{
    public EZAttack(ZombieBehaviour zombieSm) : base("Attack", zombieSm) { }

    
    private bool _hasAttacked;
    private float _attackCoolDown = 2f;
    private float _curAttackCoolDown;


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Zombie: Enter AttackState");
        //make the zombie stop moving
        ((ZombieBehaviour) fsm).navAgent.SetDestination(((ZombieBehaviour) fsm).transform.position);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
        ((ZombieBehaviour)fsm).transform.LookAt(((ZombieBehaviour)fsm).player);

        if (!_hasAttacked)
        {
            var hp = ((ZombieBehaviour)fsm).player.GetComponent<IDamageable>();

            if (hp != null)
            {
                Debug.Log("HP Player DOWN");
                hp.Damage(((ZombieBehaviour)fsm).damage);
            }

            _hasAttacked = true;
            ResetAttack();
        }
        
        
        if (((ZombieBehaviour)fsm).playerInDetectRange && !((ZombieBehaviour)fsm).playerInAttackRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).chaseState);
        }
        Debug.Log("Zombie: Update AttackState");
    }

    private void ResetAttack()
    {
        while(_hasAttacked == true)
        {
            _curAttackCoolDown -= Time.deltaTime;
            if (_curAttackCoolDown <= 0f)
            {
                _curAttackCoolDown = _attackCoolDown;
               break;
            }
        }

        _hasAttacked = false;
    }
}
