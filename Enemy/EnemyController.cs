using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    const float locomationAnimationSmoothTime = 0.1f;

    public float baseOffset = 0.1f; // Set a base offset of 0.5 units

    public float minDistance = 5f;
    public float maxDistance = 10f;
    public float speed = 2f;
    private bool isMoving = true;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    public Animator animator;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        controller = GetComponent<CharacterController>();
        // agent.baseOffset = baseOffset;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("InCombat", combat.InCombat);
        if (isMoving && combat.InCombat == false)
        {
            animator.SetFloat("Speed", 0.5f);
            // Move the enemy in a random direction
            Vector3 randomDirection =
                Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1);
            Vector3 finalPosition = hit.position;

            // Set the enemy's destination and move towards it with controller
            controller.SimpleMove((finalPosition - transform.position).normalized * speed);
            //rotate the enemy to the direction of its movement
            transform.rotation = Quaternion.LookRotation(
                controller.velocity.normalized,
                Vector3.up
            );

            // Set isMoving to false
            isMoving = false;
        }
        else
        {
            // Wait for a random amount of time before moving again
            float randomTimer = Random.Range(2f, 5f);
            StartCoroutine(WaitAndMove(randomTimer));
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            animator.SetBool("Walk Forward", false);
            animator.SetBool("Run Forward", true);

            //create a vector thats close to the target but not on it
            Vector3 attackPosition = target.position + target.forward * 1.5f;
            //move the enemy to the attack position
            // agent.SetDestination(attackPosition);
            // FaceTarget();
            //
            FaceTarget();

            if (distance <= 2)
            {
                animator.SetFloat("Speed", 0);

                // Stop moving
                // agent.SetDestination(transform.position);

                // Stop moving
                animator.SetTrigger("Attack");
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                combat.Attack(targetStats);

                // Attack the target
                if (targetStats != null) { }
                // Attack the target
            }
        }
    }

    IEnumerator WaitAndMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * 5f
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
