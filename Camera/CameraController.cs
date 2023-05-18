using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public PlayerInput playerInput;

    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 1f;
    public float maxZoom = 10f;
    public float yawSpeed = 100f;

    public float currentZoom = 1.52f;
    public float currentYaw = 0f;

    // Start is called before the first frame update

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        //only works with left and right arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentYaw -= yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentYaw += yawSpeed * Time.deltaTime;
        }

        //rotate camera with swipe


        //pinch to zoom



        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     //if zoomed out all the way
        //     if (pitch < 15)
        //     {
        //         pitch += 0.1f;
        //     }
        // }

        // if (Input.GetKey(KeyCode.DownArrow))
        // {
        //     //lower pttch until we get to 2f
        //     if (pitch > 2)
        //     {
        //         pitch -= 0.1f;
        //     }
        // }
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
