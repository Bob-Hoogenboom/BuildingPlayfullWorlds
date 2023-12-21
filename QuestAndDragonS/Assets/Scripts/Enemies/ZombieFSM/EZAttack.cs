using UnityEditor.Timeline.Actions;
using UnityEngine;
using System.Collections;

public class EZAttack : State
{
    public EZAttack(ZombieBehaviour zombieSm) : base("Attack", zombieSm) { }

    private bool _hasAttacked;


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
            ((ZombieBehaviour)fsm).StartCoroutine(ResetAttack());
        }
        
        if (((ZombieBehaviour)fsm).playerInDetectRange && !((ZombieBehaviour)fsm).playerInAttackRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).chaseState);
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(((ZombieBehaviour)fsm).timeBetweenAttacks);
        _hasAttacked = false;
    }

/*    private void ResetAttack()
    {
        while (_hasAttacked == true)
        {
            Debug.Log("resetAttack" + _curAttackCoolDown);
            _curAttackCoolDown -= Time.deltaTime;
            if (_curAttackCoolDown <= 0f)
            {
                Debug.Log("resetting. . . " + _curAttackCoolDown);
                _curAttackCoolDown = _attackCoolDown;
               break;
            }
            break;
        }

        _hasAttacked = false;
    }*/
}
