using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController1 : MonoBehaviour
{
    [Tooltip("The main player object")]
    public GameObject mainPlayer;
    [Tooltip("The distance at which the enemy will start attacking the main player")]
    public float attackRange = 2f;
    [Tooltip("The speed at which the enemy will move")]
    public float moveSpeed = 3f;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the main player
        float distanceToMainPlayer = Vector3.Distance(transform.position, mainPlayer.transform.position);

        // If the main player is within attack range, attack them
        if (distanceToMainPlayer <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        // Otherwise, move towards the main player
        else
        {
            navMeshAgent.SetDestination(mainPlayer.transform.position);
            navMeshAgent.speed = moveSpeed;
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
    }
}