using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : FSM
{
    [HideInInspector]
    public EZIdle idleState;
    public EZPatrol patrolState;

    //Define NavMesh/NavMeshAgent
    
    private void Awake()
    {
        idleState = new EZIdle(this);
        patrolState = new EZPatrol(this);
    }

    protected override State GetInitialState()
    {
        return idleState;
    }
}
