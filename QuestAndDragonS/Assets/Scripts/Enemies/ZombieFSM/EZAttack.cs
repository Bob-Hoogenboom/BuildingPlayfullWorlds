using UnityEditor.Timeline.Actions;
using UnityEngine;

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
            Debug.Log("ATTACK!");
            
            _hasAttacked = true;
            ((ZombieBehaviour) fsm).Invoke(nameof(ResetAttack), ((ZombieBehaviour) fsm).timeBetweenAttacks);
        }
        
        
        if (((ZombieBehaviour)fsm).playerInDetectRange && !((ZombieBehaviour)fsm).playerInAttackRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).chaseState);
        }
        Debug.Log("Zombie: Update AttackState");
    }

    private void ResetAttack()
    {
        _hasAttacked = false;
    }
}
