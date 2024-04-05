using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SunFlower : PlantHandler
{
    private EquipSystem _equipSystem;
    
    public UnityEvent onShoot;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float flashRadius = 4f;
    [SerializeField] private float damage = .25f;

    public override void OnFire(InputAction.CallbackContext context)
    {
        onShoot.Invoke();
        
        Collider[] hitColliders = Physics.OverlapSphere (transform.position, flashRadius, enemyMask);
        foreach (var hitCollider in hitColliders)
        {
            var hp = hitCollider.gameObject.GetComponent<IDealDamage>();

            if (hp != null)
            {
                hp.Damage(damage);
            }
        }
    }
    
    public override void OnAttachedCarrier(EquipSystem attachedHandler)
    {
        _equipSystem = attachedHandler;
    }

    public override void OnEquip()
    {
        _equipSystem.GetAnimator().SetTrigger("Equip");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, flashRadius);
    }
}
