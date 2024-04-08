/// <summary>
/// Interafec class containing all Interfaces
/// </summary>

//Interfaces for dealing damage
public interface IDealDamage
{ 
    public void Damage (float amount);
}

//Interface for having HitPoints 
public interface IHaveHP
{
    public float HitPoints { get; set; }
}

//Interface for statemachines to not rely on knowing what states it contains
public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
    public void ToNextState(IState nextState);
}