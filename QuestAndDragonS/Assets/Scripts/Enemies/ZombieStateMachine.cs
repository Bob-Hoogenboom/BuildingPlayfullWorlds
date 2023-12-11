using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class ZombieStateMachine : StateManager<ZombieStateMachine.EZombieState>
{
    public enum EZombieState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Death
    }

    public EZombieState state;

    private void Awake()
    {
        _currentState = _states[EZombieState.Idle];
    }
}
