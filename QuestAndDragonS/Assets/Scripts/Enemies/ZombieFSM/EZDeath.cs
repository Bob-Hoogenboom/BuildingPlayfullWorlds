using UnityEngine;

public class EZDeath : State
{
    public EZDeath(ZombieBehaviour zombieSm) : base("Death", zombieSm) { }

    private Vector3 _deathRotation = new Vector3(0f,0f,90f);

    public override void Enter()
    {
        base.Enter();
        ((ZombieBehaviour)fsm).transform.Rotate(_deathRotation);
    }
}
