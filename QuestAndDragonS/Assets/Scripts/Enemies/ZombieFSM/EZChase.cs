using System.Collections;
using System.Collections.Generic;
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
        //transition to idle state when moved to a waypoint
        Debug.Log("Zombie: Update ChaseState");
    }
}
