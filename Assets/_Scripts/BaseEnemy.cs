using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IAttackable, IAttacker
{
    [SerializeField] protected int health = 50;
    [SerializeField] protected int defaultReceivedDamage = 5;
    [SerializeField] protected int defaultDealtDamage = 5;
    [SerializeField] protected int resourcesToAdd = 200;

    private GameObject objective;
    private Animator animator;
    protected GameManager gameManager;
    private EnemySpawner enemySpawner;

    void OnEnable()
    {
        objective = GameObject.FindGameObjectWithTag("Objective");
        if (objective == null)
        {
            Debug.LogError("Objective not found");
            return;
        }

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (objective == null)
        {
            Debug.LogError("GameManager not found");
            return;
        }

        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner not found");
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

    public virtual void OnDestroy()
    {
        gameManager.ModifyResources(resourcesToAdd);
        enemySpawner.RemoveEnemy(gameObject);
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
