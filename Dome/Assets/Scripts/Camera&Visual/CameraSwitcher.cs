using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase cameraToEnable;
    [SerializeField] private CinemachineVirtualCameraBase cameraToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Enable the new camera
            cameraToEnable.Priority = 10;
            // Disable the previous camera by lowering its priority
            cameraToDisable.Priority = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Revert priorities if you want to switch back when the player exits the trigger
            cameraToEnable.Priority = 5;
            cameraToDisable.Priority = 15;
        }
    }
}
