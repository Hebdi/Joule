using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{
    public PlayerStats statsScript; // Reference to the PlayerStats instance
    public HealthBar healthBar;
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        // Ensure statsScript is not null
        if (statsScript == null)
        {
            Debug.LogError("PlayerStats reference is missing in Recharge script.");
            return;
        }

        if (other.CompareTag("EnergyRefill"))
        {
            if (statsScript.currentHealth < 800) // Assuming 800 is the refill threshold
            {
                GainHealth(200); // Adjust according to your desired refill amount
                Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
            }
            else
            {
                statsScript.currentHealth = statsScript.maxHealth; // Set to max health
                Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
                Debug.Log("You are fully charged!");
            }

            // Update health bar based on the current health of the PlayerStats
            healthBar.SetHealth(statsScript.currentHealth);
        }
    }

    // Gain Health according to the value set above
    void GainHealth(int heal)
    {
        statsScript.currentHealth += heal;
        statsScript.currentHealth = Mathf.Min(statsScript.currentHealth, statsScript.maxHealth); // Ensure health doesn't exceed maxHealth
        healthBar.SetHealth(statsScript.currentHealth);
        Debug.Log("Your battery gained 20 charge!");
    }
}
