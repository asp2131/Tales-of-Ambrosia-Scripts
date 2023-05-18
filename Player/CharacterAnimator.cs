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

    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        combat = GetComponent<CharacterCombat>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAnimationClips = defaultAnimationClips;
        combat.OnMagicAttack += OnMagicAttack;
        combat.OnSwordAttack += OnSwordAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //trigger jump animation when player is in the air
        if (jump.groundPlayer == false && jump.playerVelocity.y > 0)
        {
            animator.Play("boy_jump_default");
        }
        // if (jump.groundPlayer == true)
        // {
        //     animator.SetBool("Jumping", !jump.groundPlayer);
        // }

        //if k key is pressed, trigger the attack animation
        if (Input.GetKeyDown(KeyCode.K))
        {
            SlingShotAnimation();
        }

        animator.SetFloat("Speed", agent.speed, locomationAnimationSmoothTime, Time.deltaTime);
    }

    public void JumpAnimation(bool isJumping)
    {
        animator.SetBool("Jumping", !isJumping);
        // animator.SetBool("Grounded", true);

        // animator.SetBool("Grounded", true);
    }

    void SlingShotAnimation()
    {
        // animator.SetBool("InCombat", true);
        print(combat.InCombat);
        animator.Play("magic");
    }

    protected virtual void OnMagicAttack()
    {
        animator.SetTrigger("magic");
    }

    protected virtual void OnSwordAttack()
    {
        animator.SetTrigger("hammer");
    }
}


//This was in OnMagicAttack() in CharacterCombat.cs
//     int attackIndex = Random.Range(0, currentAnimationClips.Length);
//     // overrideController[replaceableAnimations.name] = currentAnimationClips[attackIndex];
//     //loop through all the replaceable animations
//     for (int i = 0; i < replaceableAnimations.Length; i++)
//     {
//         //if the current replaceable animation is the same as the one we are looking for
//         if (replaceableAnimations[i].name == currentAnimationClips[attackIndex].name)
//         {
//             //set the override controller to the current animation clip
//             overrideController[replaceableAnimations[i].name] = currentAnimationClips[
//                 attackIndex
//             ];
//         }
//     }
// }
