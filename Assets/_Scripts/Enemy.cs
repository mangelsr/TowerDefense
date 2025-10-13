using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject objective;
    [SerializeField] int health = 75;
    [SerializeField] int weakThreshold = 25;

    Animator animator;

    void Awake()
    {
        GetComponent<NavMeshAgent>().SetDestination(objective.transform.position);
        animator = GetComponent<Animator>();
        animator.SetBool("IsMoving", true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objective")
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("OnObjectiveReached");
        }
    }

    public void DealDamage()
    {
        if (objective == null)
        {
            animator.SetTrigger("OnObjectiveDestroyed");
            return;
        }
        objective.GetComponent<Objective>().ReceiveDamage(40);
    }

    public void ReceiveDamage(int damage = 5)
    {
        health -= damage;
        if (!animator.GetBool("IsWeak") && health < weakThreshold)
        {
            animator.SetBool("IsWeak", true);
        }
    }
}
