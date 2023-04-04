using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController1 : MonoBehaviour
{
    [Tooltip("The gameobject to focus the camera on.")]
    public GameObject targetObject;

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = targetObject.transform;
        virtualCamera.LookAt = targetObject.transform;
    }
}