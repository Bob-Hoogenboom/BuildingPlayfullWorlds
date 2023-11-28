using UnityEngine;

/// <summary>
/// An Abstract State-machine that can be utilized by multiple different enemies/NPCs
/// source:
/// https://www.youtube.com/watch?app=desktop&v=-VkezxxjsSE 
/// </summary>

public class FSM : MonoBehaviour
{
    private State currentState;

    private void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    private void Update()
    {
        if(currentState != null)
            currentState.UpdateLogic();
    }

    public void SwitchState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual State GetInitialState()
    {
        return null;
    }
    
    //debug stuff
    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(No Current State)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
