using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipedObject : PlayerInputActions.IPlayerActions
{
    void OnAttachedCarrier(EquipSystem attachedHandler);
    void OnEquip();
    void OnUnEquip();
}
