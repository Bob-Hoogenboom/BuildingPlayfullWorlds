using UnityEngine;

public class EZAttack : State
{
    public EZAttack(ZombieBehaviour zombieSm) : base("Attack", zombieSm) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Zombie: Enter AttackState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
        if (((ZombieBehaviour)fsm).playerInDetectRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).chaseState);
        }
        Debug.Log("Zombie: Update AttackState");
    }
}
