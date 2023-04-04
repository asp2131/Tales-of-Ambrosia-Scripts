using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/* Controls the player. Here we chose our "focus" and where to move. */

public class PlayerMovement : MonoBehaviour
{
    public delegate void OnFocusChanged(Interactable newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public PlayerInput playerInput;

    public CharacterController controller;

    private GameObject _EffectFootstep;

    public float speed = 0f;

    public Interactable focus; // Our current focus: Item, Enemy etc.

    public LayerMask movementMask; // The ground
    public LayerMask interactionMask; // Everything we can interact with

    public float velocity = 0f;

    Camera cam; // Reference to our camera

    CameraController cameraController;

    // Get references
    void Start()
    {
        cam = Camera.main;
        if (cam != null)
        {
            cameraController = cam.GetComponent<CameraController>();
        }

        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        _EffectFootstep = Resources.Load<GameObject>("Prefabs/Effect/EffectFootstep");
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RotateCamera();
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cam.transform.right + move.z * cam.transform.forward;
        move.y = 0f;

        //if moving increase speed
        if (move != Vector3.zero)
        {
            speed += 5f;
            if (speed > 5f)
            {
                speed = 5f;
            }
        }
        else
        {
            speed -= 5f;
            if (speed < 0f)
            {
                speed = 0f;
            }
        }

        controller.Move(move * speed * Time.deltaTime);
    }

    private void FootstepEffect()
    {
        GameObject effect = Instantiate<GameObject>(_EffectFootstep);
        effect.transform.position =
            this.transform.position + this.transform.rotation * new Vector3(0, 0, -0.05f);
    }

    void RotatePlayer()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cam.transform.right + move.z * cam.transform.forward;
        move.y = 0f;

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(move),
                0.15F
            );
        }
    }

    void RotateCamera()
    {
        Vector2 lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        if (lookInput.x > 0)
        {
            cameraController.currentYaw -= cameraController.yawSpeed * Time.deltaTime;
        }

        if (lookInput.x < 0)
        {
            cameraController.currentYaw += cameraController.yawSpeed * Time.deltaTime;
        }

        if (lookInput.x == 0 && lookInput.y == 0 && moveInput.x == 0 && moveInput.y == 0)
        {
            //pinch to zoom in and out
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;
                cameraController.currentZoom -=
                    difference * cameraController.zoomSpeed * Time.deltaTime;
                cameraController.currentZoom = Mathf.Clamp(
                    cameraController.currentZoom,
                    cameraController.minZoom,
                    cameraController.maxZoom
                );
            }
        }
    }
}


/*
Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cam.transform.right + move.z * cam.transform.forward;
        move.y = 0f;
        controller.Move(move * speed * Time.deltaTime);

*/
