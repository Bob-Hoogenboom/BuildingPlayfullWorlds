using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    public Vector3 lookRot;
    private float _currentLifeTime;

    private void Update()
    {
        if (_currentLifeTime > 0)
        {
            _currentLifeTime -= Time.deltaTime;
            rb.velocity = lookRot  * speed;
        }
        else
        {
            _currentLifeTime = lifeTime;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;

        var hp = other.gameObject.GetComponent<IDamagable>();

        if (hp != null)
        {
            hp.Damage(damage);
        }

        gameObject.SetActive(false);
    }
}
