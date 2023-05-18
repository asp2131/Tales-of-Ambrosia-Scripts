using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    const float locomationAnimationSmoothTime = .1f;

    protected EnemyCombat combat;

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<EnemyCombat>();
        combat.OnAttack += OnAttack;
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("Attack");
    }
}
