using UnityEngine;

public class EZIdle : State
{
    public EZIdle(ZombieBehaviour zombieSm) : base("Idle", zombieSm) { }

    private float _timer = 3f;
    private float _curTimer;

    public override void Enter()
    {
        base.Enter();
        _curTimer = _timer;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
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
