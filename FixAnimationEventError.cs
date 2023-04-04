using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FixAnimationEventError : MonoBehaviour
{
    [Tooltip("The name of the GameObject that should receive the AnimationEvent.")]
    [SerializeField] private string receiverName = "FootstepEffectReceiver";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Find the GameObject with the specified name and add it as a receiver for the AnimationEvent.
        GameObject receiver = GameObject.Find(receiverName);
        if (receiver != null)
        {
            AnimationEvent footstepEvent = new AnimationEvent();
            footstepEvent.functionName = "PlayFootstepEffect";
            footstepEvent.time = 0.5f;
            footstepEvent.messageOptions = SendMessageOptions.RequireReceiver;
            receiver.SendMessage("PlayFootstepEffect", footstepEvent, SendMessageOptions.RequireReceiver);

            // Add the AnimationEvent to the animation clip.
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
            clip.AddEvent(footstepEvent);
        }
        else
        {
            Debug.LogError("Could not find GameObject with name " + receiverName);
        }
    }

    // This method is called by the AnimationEvent.
    private void PlayFootstepEffect()
    {
        // Play the footstep effect.
    }
}