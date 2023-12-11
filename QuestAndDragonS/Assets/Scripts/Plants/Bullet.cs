using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage;
    private float currentLifeTime;
    public Vector3 lookRot;

    private void Update()
    {
        if (currentLifeTime > 0)
        {
            currentLifeTime -= Time.deltaTime;
            rigidbody.velocity = lookRot  * speed;
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
        Health hp = other.gameObject.GetComponent<Health>();

        if (hp != null)
        {
            Debug.Log("HP DOWN");
        }
        
        gameObject.SetActive(false);
    }
}
