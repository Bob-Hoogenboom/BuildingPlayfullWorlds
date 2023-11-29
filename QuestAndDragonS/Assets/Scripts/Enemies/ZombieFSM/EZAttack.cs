using System.Collections;
using System.Collections.Generic;
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
        //transition to idle state when moved to a waypoint
        Debug.Log("Zombie: Update AttackState");
    }
}
