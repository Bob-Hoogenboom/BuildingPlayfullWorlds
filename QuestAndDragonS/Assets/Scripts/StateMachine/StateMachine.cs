/// <summary>
/// Generic FSM that does not rely on knowing what states it contains via an Interface Class
/// SRC: https://forum.unity.com/threads/how-should-i-store-transition-tables-for-an-fsm.496122/ 
/// </summary>

public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Execute();
    }
}
