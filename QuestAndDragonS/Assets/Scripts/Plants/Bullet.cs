using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    private float currentLifeTime;
    public Vector3 lookRot;

    private void Update()
    {
        if (currentLifeTime > 0)
        {
            currentLifeTime -= Time.deltaTime;
            rb.velocity = lookRot  * speed;
        }
        else
        {
            currentLifeTime = lifeTime;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;

        var hp = other.gameObject.GetComponent<IDealDamage>();

        if (hp != null)
        {
            Debug.Log("HP DOWN");
            hp.Damage(damage);

        }
        
        gameObject.SetActive(false);
    }
}
