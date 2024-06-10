using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthItem : MonoBehaviour
{
    public UnityEvent onPickUp;

    [SerializeField] private Vector3 rotation;
    [SerializeField] private float rotateSpeed = 5.0f;
    [SerializeField] private int restoreAmount = 10;

    private IDamagable _iDamagable;



    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!collision.gameObject.CompareTag("Player")) return;

        _iDamagable = collision.gameObject.GetComponent<IDamagable>();
        if (_iDamagable == null) return;

        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        _iDamagable.Damage(-restoreAmount);
        onPickUp.Invoke();

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
