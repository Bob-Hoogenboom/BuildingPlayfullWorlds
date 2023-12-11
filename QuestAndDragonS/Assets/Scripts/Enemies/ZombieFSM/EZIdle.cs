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
        Debug.Log(_curTimer);
        if (((ZombieBehaviour)fsm).playerInDetectRange)
        {
            fsm.SwitchState(((ZombieBehaviour)fsm).chaseState);
        }

        if (_curTimer <= 0)
        {
            _curTimer = _timer;
            fsm.SwitchState(((ZombieBehaviour)fsm).patrolState);
        }
        
        _curTimer -= Time.deltaTime;
    }
}
