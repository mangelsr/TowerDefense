using UnityEngine;

public class Objective : MonoBehaviour, IAttackable
{
    [SerializeField] private int health = 100;
    [SerializeField] private int defaultReceivedDamage = 20;

    public delegate void ObjectiveDestroyed();
    public event ObjectiveDestroyed OnObjectiveDestroyed;

    void Update()
    {
        if (health <= 0)
        {
            if (OnObjectiveDestroyed != null)
            {
                OnObjectiveDestroyed();
            }
            Destroy(this.gameObject);
        }
    }

    public void ReceiveDamage(int damage = 0)
    {
        int receivedDamage = damage == 0 ? defaultReceivedDamage : damage;
        health -= receivedDamage;
    }
}
