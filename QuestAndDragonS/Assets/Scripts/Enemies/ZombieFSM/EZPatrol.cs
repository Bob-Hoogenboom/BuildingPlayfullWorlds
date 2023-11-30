using UnityEngine;

public class EZPatrol : State
{
    public EZPatrol(ZombieBehaviour zombieSm) : base("Patrol", zombieSm) { }

    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private float _walkPointRange;
    
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Zombie: Enter PatrolState");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Debug.Log("Zombie: Update PatrolState");
    }
}
