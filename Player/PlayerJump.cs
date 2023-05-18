using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    public CharacterController _characterController;

    private PlayerInput _playerInput;

    public Vector3 playerVelocity;

    public bool groundPlayer = true;

    [SerializeField]
    private float _jumpHeight = 5.0f;

    public bool jumpPressed = false;

    [SerializeField]
    public float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Jump"].performed += ctx => OnJump();
    }

    // Update is called once per frame

    void Update()
    {
        if (groundPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (jumpPressed)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * gravityValue);
            jumpPressed = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void OnJump()
    {
        if (groundPlayer)
        {
            jumpPressed = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            groundPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            groundPlayer = false;
        }
    }
}
