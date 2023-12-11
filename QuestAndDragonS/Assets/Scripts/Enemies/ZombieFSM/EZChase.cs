using UnityEngine;

public class EZChase : State
{
    public EZChase(ZombieBehaviour zombieSm) : base("Chase", zombieSm) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Zombie: Enter ChaseState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        ((ZombieBehaviour) fsm).navAgent.SetDestination(((ZombieBehaviour) fsm).player.position);
        
        if (((ZombieBehaviour)fsm).playerInAttackRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).attackState);
        }
        else if (!((ZombieBehaviour)fsm).playerInDetectRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).idleState);
        }
        Debug.Log("Zombie: Update ChaseState");
    }
}
