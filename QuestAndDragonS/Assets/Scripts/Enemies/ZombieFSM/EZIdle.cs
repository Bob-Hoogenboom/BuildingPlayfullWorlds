using System.Threading;
using UnityEngine;

public class EZIdle : State
{
    public EZIdle(ZombieBehaviour zombieSm) : base("Idle", zombieSm) { }

    private float _timer = 5f;
    private float _curTimer;

    public override void Enter()
    {
        base.Enter();
        _curTimer = _timer;
        Debug.Log("Zombie: Enter IdleState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //transition to the patrol state when the 'wait' timer has run out

        if (_curTimer <= 0)
        {
            _curTimer = _timer;
            fsm.SwitchState(((ZombieBehaviour)fsm).patrolState);
        }
        else
        {
            _curTimer -= Time.deltaTime;
        }
        Debug.Log("Zombie: Update IdleState");
    }
}
