using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private Vector2 _movementValue;

    //Doesnt get recognized by rider but works anyway
    private void OnMovement(InputValue inputValue)
    {
        if (inputValue == Vector2.zero)
        {
            _movementValue = inputValue.Get<Vector2>();
        }
    }

    private void Update()
    {
        print(_movementValue);
    }
}
