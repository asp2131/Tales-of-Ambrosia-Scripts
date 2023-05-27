using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/* Controls the player. Here we chose our "focus" and where to move. */

public class PlayerMovement : MonoBehaviour
{
    // public delegate void OnFocusChanged(Interactable newFocus);
    // public OnFocusChanged onFocusChangedCallback;

    public PlayerInput playerInput;

    public CharacterController controller;

    private GameObject _EffectFootstep;

    public float speed = 0f;

    public Interactable focus; // Our current focus: Item, Enemy etc.

    public LayerMask movementMask; // The ground
    public LayerMask interactionMask; // Everything we can interact with

    public float velocity = 0f;

    Transform target;

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
        // playerInput.actions["Move"].performed += ctx => OnMove();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RotateCamera();
        OnMove();

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray;
            //if android or ios
            if (
                Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer
            )
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
                ray = cam.ScreenPointToRay(Input.touches[0].position);
            }
            else
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Move our player to what we hit with our character controller

                //Check to see if we click an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        //if target is out of range, stop following
        if (target != null && Vector3.Distance(transform.position, target.position) > 5f)
        {
            target = null;
            RemoveFocus();
        }
        if (target != null)
        {
            // move to the target
            //attack the target
            FaceTarget();
        }
    }

    void OnMove()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cam.transform.right + move.z * cam.transform.forward;
        move.y = 0f;

        //if moving increase speed
        if (move != Vector3.zero)
        {
            //if platform is android or ios
            if (
                Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer
            )
            {
                //if joystick is not being used, return
                speed += 3f;
                if (speed > 3f)
                {
                    speed = 3f;
                }
            }
            else
            {
                speed += 5f;

                speed += 5f;
                if (speed > 5f)
                {
                    speed = 5f;
                }
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

    void MoveToTarget(Vector3 point)
    {
        //if using joystick, move player to point that will increase speed
        controller.Move(point * speed * Time.deltaTime);
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

    public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.transform;
        //        target = newTarget.interactionTransform;
    }

    void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            // motor.StopFollowingTarget();
        }
        focus = newFocus;
        newFocus.OnFocused(transform);
        FollowTarget(newFocus);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
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
