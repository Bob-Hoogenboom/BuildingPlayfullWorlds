using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterHandler : MonoBehaviour, IEquipedObject
{

    private EquipSystem _equipSystem;
    public void OnMove(InputAction.CallbackContext context)
    { 
    }

    public void OnFire(InputAction.CallbackContext context)
    { 
    }

    public void OnSwapPlant(InputAction.CallbackContext context)
    { 
    }

    public void OnLookMouse(InputAction.CallbackContext context)
    {
    }

    public void OnLookStick(InputAction.CallbackContext context)
    {
    }
    public void OnDodgeRoll(InputAction.CallbackContext context)
    {
    }


    public void OnAttachedCarrier(EquipSystem attachedHandler)
    {
        _equipSystem = attachedHandler;
    }

    public void OnEquip()
    {
        _equipSystem.GetAnimator().SetTrigger("Equip");
    }

    public void OnUnEquip()
    {
        
    }

}
