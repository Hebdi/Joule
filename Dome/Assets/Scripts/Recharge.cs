using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{
    public PlayerStats SatsScript;

    public static int currentHealth;
    public HealthBar healthBar;

    public GameObject pickupEffect;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyRefill") && PlayerStats.currentHealth < 80)
        {
            GainHealth(20);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);

        }

        else if (other.CompareTag("EnergyRefill") && PlayerStats.currentHealth >= 80)
        {
            currentHealth = 100;
            Destroy(other.gameObject);
            Debug.Log("You are fully charged!");
        }
    }

    // Gain Health according to the value set above
    void GainHealth(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Your battery gained 20 charge!");
    }

}
