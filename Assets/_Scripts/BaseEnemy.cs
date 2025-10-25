using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IAttackable, IAttacker
{
    [SerializeField] protected int health = 50;
    [SerializeField] protected int defaultReceivedDamage = 5;
    [SerializeField] protected int defaultDealtDamage = 5;


    GameObject objective;
    Animator animator;

    void OnEnable()
    {
        objective = GameObject.FindGameObjectWithTag("Objective");
        if (objective == null)
        {
            Debug.LogError("Objective not found");
            return;
        }

        objective.GetComponent<Objective>().OnObjectiveDestroyed += Stop;
    }

    void OnDisable()
    {
        if (objective != null)
        {
            objective.GetComponent<Objective>().OnObjectiveDestroyed -= Stop;
        }
    }

    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(objective.transform.position);
        animator = GetComponent<Animator>();
        animator.SetBool("IsMoving", true);
    }

    void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("OnDeath");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Destroy(gameObject, 3);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objective")
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("OnObjectiveReached");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void DealDamage(int damage = 0)
    {
        if (objective == null)
        {
            animator.SetTrigger("OnObjectiveDestroyed");
            return;
        }

        int damageToDeal = damage == 0 ? defaultDealtDamage : damage;
        objective.GetComponent<Objective>().ReceiveDamage(damageToDeal);
    }

    public void ReceiveDamage(int damage = 0)
    {
        int damageToReceive = damage == 0 ? defaultReceivedDamage : damage;
        health -= damageToReceive;
    }

    public void Stop()
    {
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("OnObjectiveDestroyed");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }
}
