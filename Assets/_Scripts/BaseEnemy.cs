using System.Collections;
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
    private bool isDead = false;

    void OnEnable()
    {
        objective = GameObject.FindGameObjectWithTag("Objective");
        if (objective == null)
        {
            Debug.LogError("Objective not found");
            return;
        }

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
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
        if (health <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objective" && !isDead)
        {
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("OnObjectiveReached");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public void DealDamage(int damage = 0)
    {
        if (objective == null || isDead) return;

        int damageToDeal = damage == 0 ? defaultDealtDamage : damage;
        objective.GetComponent<Objective>().ReceiveDamage(damageToDeal);
    }

    public void ReceiveDamage(int damage = 0)
    {
        if (isDead) return;

        int damageToReceive = damage == 0 ? defaultReceivedDamage : damage;
        health -= damageToReceive;
    }

    public void Stop()
    {
        if (isDead) return;

        animator.SetBool("IsMoving", false);
        animator.SetTrigger("OnObjectiveDestroyed");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    private IEnumerator Die()
    {
        isDead = true;

        GetComponent<NavMeshAgent>().SetDestination(transform.position);
        GetComponent<NavMeshAgent>().isStopped = true;

        gameManager.ModifyResources(resourcesToAdd);
        enemySpawner.RemoveEnemy(gameObject);

        OnEnemyDeath();

        GetComponent<Collider>().enabled = false;

        animator.SetTrigger("OnDeath");

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

    protected virtual void OnEnemyDeath()
    {
    }
}