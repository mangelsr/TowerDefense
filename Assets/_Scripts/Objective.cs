using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] int health = 100;

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

    public void ReceiveDamage(int damage = 20)
    {
        health -= damage;
    }
}
