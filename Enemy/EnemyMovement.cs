using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [Tooltip("The minimum distance the enemy will move from its current position.")]
    public float minDistance = 5f;

    [Tooltip("The maximum distance the enemy will move from its current position.")]
    public float maxDistance = 10f;

    [Tooltip("The time in seconds the enemy will wait before moving to a new location.")]
    public float waitTime = 2f;

    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 targetPosition;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetNewTargetPosition();
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitBeforeMoving());
        }
    }

    private IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(waitTime);
        SetNewTargetPosition();
    }

    private void SetNewTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1);
        targetPosition = hit.position;
        agent.SetDestination(targetPosition);
    }
}
