using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlantObject : PlayerInputActions.IPlayerActions
{
    void OnAttachedCarrier(PlantHandler attachedHandler);
    void OnEquip();
    void OnUnEquip();
}
