using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorZombie : MonoBehaviour
{
    private StateMachine stateMachine = new StateMachine();

    private void Start()
    {
        stateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
