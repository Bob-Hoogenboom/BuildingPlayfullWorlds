using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float hitPoints;
    public UnityEvent onDamage;
    public UnityEvent onDestroy;

    public void TakeDamage(float auwie)
    {
        hitPoints -= auwie;

        onDamage.Invoke();
        
        if (hitPoints <=0)
        {
            onDestroy.Invoke();
        }
    }
}
