using UnityEngine;

public class Bullet : MonoBehaviour, IAttacker
{
    public Vector3 endPosition;
    public GameObject enemy;
    [SerializeField] private float speed = 25;
    [SerializeField] private int defaultDamage = 10;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
        if (Vector3.Distance(transform.position, endPosition) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = collision.gameObject;
            DealDamage(defaultDamage);
            Destroy(gameObject);
        }
    }

    public void DealDamage(int damage = 0)
    {
        enemy.GetComponent<BaseEnemy>().ReceiveDamage(damage);
    }
}
