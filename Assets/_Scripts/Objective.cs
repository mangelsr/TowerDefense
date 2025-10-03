using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] int health = 100;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ReceiveDamage(int damage = 20)
    {
        health -= damage;
    }
}
