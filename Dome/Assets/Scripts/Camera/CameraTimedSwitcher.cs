using UnityEngine;
using Cinemachine;
using System.Collections;  // Add this directive for IEnumerator

public class CameraTimedSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase cameraToEnable;
    [SerializeField] private CinemachineVirtualCameraBase cameraToDisable;
    [SerializeField] private float switchDuration = 5f;  // Duration for the camera switch

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the coroutine to switch the camera for a duration
            StartCoroutine(SwitchCameraTemporarily());
        }
    }

    private IEnumerator SwitchCameraTemporarily()
    {
        // Enable the new camera
        cameraToEnable.Priority = 10;
        // Disable the previous camera by lowering its priority
        cameraToDisable.Priority = 5;

        // Wait for the specified duration
        yield return new WaitForSeconds(switchDuration);

        // Switch back to the original camera
        cameraToEnable.Priority = 5;
        cameraToDisable.Priority = 10;

        // Destroy this game object after switching back
        Destroy(gameObject);
    }
}
