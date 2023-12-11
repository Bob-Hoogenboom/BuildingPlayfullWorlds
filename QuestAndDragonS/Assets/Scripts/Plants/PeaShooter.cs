using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PeaShooter : PlantHandler
{
    private EquipSystem _equipSystem;
    
    public UnityEvent onShoot;
    [SerializeField] private Transform bulletSpawn;
    

    public override void OnFire(InputAction.CallbackContext context)
    {
        onShoot.Invoke();
        
        var bulletObj = BulletSpawner.instance.GetPooledObject();

        if (bulletObj == null) return;
        var bullet = bulletObj.GetComponent<Bullet>(); 
        bulletObj.transform.position = bulletSpawn.position;
        bullet.lookRot = transform.forward;
        bulletObj.SetActive(true);
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
