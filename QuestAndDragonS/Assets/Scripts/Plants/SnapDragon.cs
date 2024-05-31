using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SnapDragon : PlantHandler
{
    private EquipSystem _equipSystem;

    [Header("References")]
    [SerializeField] private GameObject shootOBJ;
    [SerializeField] private ParticleSystem fireSpray;
    [SerializeField] private ParticleSystem dust;

    [Header("Values")]
    [SerializeField] private float sprayTime = 3f;
    [SerializeField] private float sprayCooldown = 3f;
    [SerializeField] private float damageLength = 3f;
    [SerializeField] private float damage = .10f;
    public UnityEvent onShoot;

    private bool _canSpray = true;
    private bool _isSpraying = false;


    public override void OnFire(InputAction.CallbackContext context)
    {
        onShoot.Invoke();

        if (_canSpray)
        {
            StartCoroutine(FireSpray());
        }
        else if (!_isSpraying)
        {
            //Play dust particle
            dust.Play();
        }
    }

    IEnumerator FireSpray()
    {
        _canSpray = false;
        _isSpraying = true;

        fireSpray.Play();

        float currentSprayTime = sprayTime;
        while (currentSprayTime > 0) 
        {
            //Check if the hitObject has IDamagable and deal damage to the damagableOBJ via a raycast
            RaycastHit hit;
            Vector3 debugDir = shootOBJ.transform.position + transform.forward * damageLength;

            Debug.DrawLine(shootOBJ.transform.position, debugDir, Color.magenta, 1f);

            if (Physics.Raycast(shootOBJ.transform.position, transform.forward, out hit, damageLength))
            {
                IDamagable damagable =  hit.transform.gameObject.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.Damage(damage);
                }
            }

            currentSprayTime -= Time.deltaTime;
            yield return null;
        }

        fireSpray.Stop();
        _isSpraying = false;

        yield return new WaitForSeconds(sprayCooldown);
        _canSpray = true;

        yield return null;
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
