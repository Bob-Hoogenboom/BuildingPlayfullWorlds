using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SunFlower : PlantHandler
{
    private EquipSystem _equipSystem;
    
    public override void OnFire(InputAction.CallbackContext context)
    {
        
    }
    
    public override void OnAttachedCarrier(EquipSystem attachedHandler)
    {
        _equipSystem = attachedHandler;
    }

    public override void OnEquip()
    {
        _equipSystem.GetAnimator().SetTrigger("Equip");
    }
}
