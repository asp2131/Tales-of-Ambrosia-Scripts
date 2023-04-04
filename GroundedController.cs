using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GroundedController : MonoBehaviour
{
    [Tooltip("The maximum distance to check for ground.")]
    public float groundCheckDistance = 0.1f;

    [Tooltip("The layer mask for ground objects.")]
    public LayerMask groundMask;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // Check if the character is grounded
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask))
        {
            // If grounded, reset the vertical velocity
            controller.Move(Vector3.down * controller.velocity.y * Time.fixedDeltaTime);
        }
    }
}