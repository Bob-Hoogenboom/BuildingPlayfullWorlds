using UnityEngine;

public class EZDeath : State
{
    public EZDeath(ZombieBehaviour zombieSm) : base("Death", zombieSm) { }

    public override void Enter()
    {
        base.Enter();
        ((ZombieBehaviour)fsm).OnDeath.Invoke();
        ((ZombieBehaviour)fsm).gameObject.SetActive(false);
    }
}
