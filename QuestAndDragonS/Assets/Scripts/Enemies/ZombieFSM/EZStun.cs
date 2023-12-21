using UnityEngine;

public class EZStun : State
{
    public EZStun(ZombieBehaviour zombieSm) : base("stun", zombieSm) { }

    private float _stunTime = 3f;
    private float _curTimer;

    public override void Enter()
    {
        base.Enter();
        _curTimer = _stunTime;
        ((ZombieBehaviour)fsm).OnStunned.Invoke();
        Debug.Log("Zombie: Enter stunstate");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_curTimer <= 0)
        {
            _curTimer = _stunTime;
            fsm.SwitchState(((ZombieBehaviour)fsm).idleState);
        }
        
        _curTimer -= Time.deltaTime;
    }
}
