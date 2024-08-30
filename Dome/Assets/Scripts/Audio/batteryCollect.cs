using UnityEngine;

public class batteryCollect : MonoBehaviour
{
    [SerializeField] private GameObject emptyBatteryPrefab; // Prefab to replace with
    private bool isCollected = false; // Flag to ensure it only triggers once

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return; // If already collected, do nothing

        if (other.CompareTag("Player")) // Ensure it's the player that collects the battery
        {
            // Mark as collected to prevent multiple triggers
            isCollected = true;

            // Replace with the empty battery prefab
            ReplaceWithEmptyBattery();
        }
    }

    private void ReplaceWithEmptyBattery()
    {
        // Instantiate the empty battery prefab at the current battery's position and rotation
        Instantiate(emptyBatteryPrefab, transform.position, transform.rotation);

        // Destroy the current battery game object
        Destroy(gameObject);
    }
}
