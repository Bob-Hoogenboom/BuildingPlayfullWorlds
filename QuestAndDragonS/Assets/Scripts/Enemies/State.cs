using System;
using UnityEngine;

public class State
{
    public String name;
    protected FSM fsm;

    public State(string name, FSM fsm)
    {
        this.name = name;
        this.fsm = fsm;
    }
    
    public virtual void Enter(){}
    public virtual void UpdateLogic(){}
    public virtual void Exit(){}
}
