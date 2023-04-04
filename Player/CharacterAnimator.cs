using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip[] replaceableAnimations;
    public AnimationClip[] defaultAnimationClips;
    protected AnimationClip[] currentAnimationClips;
    const float locomationAnimationSmoothTime = 0.1f;
    PlayerMovement agent;

    PlayerJump jump;
    public Animator animator;

    float speedPercent;

    // protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAnimationClips = defaultAnimationClips;
        // combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //trigger jump animation when player is in the air
        if (jump.groundPlayer == false && jump.playerVelocity.y > 0)
        {
            JumpAnimation();
        }

        animator.SetFloat("Speed", agent.speed, locomationAnimationSmoothTime, Time.deltaTime);
    }

    public void JumpAnimation()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", true);

        // animator.SetBool("Grounded", true);
    }

    protected virtual void OnAttack()
    {
        print("OnAttack");
        // animator.SetTrigger("Attack");

        // int attackIndex = Random.Range(0, currentAnimationClips.Length);
        // // overrideController[replaceableAnimations.name] = currentAnimationClips[attackIndex];
        // //loop through all the replaceable animations
        // for (int i = 0; i < replaceableAnimations.Length; i++)
        // {
        //     //if the current replaceable animation is the same as the one we are looking for
        //     if (replaceableAnimations[i].name == currentAnimationClips[attackIndex].name)
        //     {
        //         //set the override controller to the current animation clip
        //         overrideController[replaceableAnimations[i].name] = currentAnimationClips[
        //             attackIndex
        //         ];
        //     }
        // }
    }
}
