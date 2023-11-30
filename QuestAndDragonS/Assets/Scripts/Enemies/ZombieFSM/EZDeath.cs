using UnityEngine;

public class EZDeath : State
{
    public EZDeath(ZombieBehaviour zombieSm) : base("Death", zombieSm) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Zombie: Enter DeathState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Debug.Log("Zombie: Update DeathState");
    }
}
