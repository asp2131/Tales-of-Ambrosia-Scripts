using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    const float locomationAnimationSmoothTime = 0.1f;

    public float baseOffset = 0.1f; // Set a base offset of 0.5 units

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        // agent.baseOffset = baseOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            Vector3 randomPosition = RandomNavMeshLocation(10f);
            agent.SetDestination(randomPosition);
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            animator.SetBool("Walk Forward", false);
            animator.SetBool("Run Forward", true);

            //create a vector thats close to the target but not on it
            Vector3 attackPosition = target.position + target.forward * 1.5f;
            //move the enemy to the attack position
            agent.SetDestination(attackPosition);
            // FaceTarget();
            //
            FaceTarget();

            if (distance <= 5.5 && distance >= 4)
            {
                animator.SetBool("Run Forward", false);

                // Stop moving
                agent.SetDestination(transform.position);

                // Stop moving
                animator.SetTrigger("Attack 01");
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                combat.Attack(targetStats);

                // Attack the target
                if (targetStats != null) { }
                // Attack the target
            }
        }
    }

    Vector3 RandomNavMeshLocation(float radius)
    {
        animator.SetBool("Walk Forward", true);
        //procedural walk movement for the enemy
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        Vector3 randomPosition = transform.position + randomDirection;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return transform.position;
        }

        // Vector3 randomDirection = Random.insideUnitSphere * radius;
        // randomDirection += transform.position;
        // NavMeshHit hit;
        // NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        // return hit.position;
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
