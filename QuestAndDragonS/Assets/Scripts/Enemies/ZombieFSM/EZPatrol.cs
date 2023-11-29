using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EZPatrol : State
{
    public EZPatrol(ZombieBehaviour zombieSm) : base("Patrol", zombieSm) { }

    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private float _walkPointRange;
    
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Zombie: Enter PatrolState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //transition to idle state when moved to a waypoint
        Debug.Log("Zombie: Update PatrolState");
    }
}
