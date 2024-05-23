using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlantHandler : MonoBehaviour, IEquipedObject
{
    public virtual void OnMove(InputAction.CallbackContext context)
    {
    }

    public virtual void OnFire(InputAction.CallbackContext context)
    { 
    }

    public virtual void OnSwapPlant(InputAction.CallbackContext context)
    {   
    }

    public virtual void OnLookMouse(InputAction.CallbackContext context)
    {
    }

    public virtual void OnLookStick(InputAction.CallbackContext context)
    {
    }
    public void OnDodgeRoll(InputAction.CallbackContext context)
    {
    }

    public virtual void OnAttachedCarrier(EquipSystem attachedHandler)
    {
        
    }

    public virtual void OnEquip()
    {
        
    }

    public virtual void OnUnEquip()
    {
        
    }

}

