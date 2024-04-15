/// <summary>
/// Interafec class containing all Interfaces
/// </summary>

//Interfaces for Taking damage
public interface IDamagable
{
    public float HitPoints { get; set; }
    public void Damage (float amount);
}

//Interface for statemachines to not rely on knowing what states it contains
public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
    public void ToNextState(IState nextState);
}