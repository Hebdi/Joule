using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 20;
    public static int Rounds;

    public int maxHealth = 1000; // Increased max health
    public static int currentHealth;
    public HealthBar healthBar;

    public int energyUse = 100;
    public float boostHealthDrainPerSecond = 50f; // High value for smoothness

    bool enter; // for trigger detection
    private bool isBoosting;

    public GameObject pickupEffect;

    void Start()
    {
        Lives = startLives;
        Rounds = 0;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (enter && Input.GetKeyDown(KeyCode.E))
        {
            LoseHealth(energyUse);
        }

        // Check if the player is boosting
        isBoosting = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift);
    }

    void FixedUpdate()
    {
        // Drain health if boosting and moving
        if (isBoosting && currentHealth > 0)
        {
            float drainAmount = boostHealthDrainPerSecond * Time.fixedDeltaTime;
            DrainHealth(drainAmount);
            Debug.Log($"Draining Health: {drainAmount} | Current Health: {currentHealth}");
        }
    }

    void DrainHealth(float drainAmount)
    {
        currentHealth -= Mathf.RoundToInt(drainAmount);
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died");
            this.enabled = false;
        }
    }

    void LoseHealth(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died");
            this.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyRefill") && PlayerStats.currentHealth < 800) // Adjusted max refill limit
        {
            GainHealth(200);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnergyRefill") && PlayerStats.currentHealth >= 800)
        {
            currentHealth = 1000; // Set to max health
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Debug.Log("You are fully charged!");
            healthBar.SetHealth(currentHealth);
        }

        if (other.CompareTag("NPC"))
        {
            enter = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("NPC"))
        {
            enter = false;
        }
    }

    void GainHealth(int heal)
    {
        currentHealth += heal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth);
        Debug.Log("Your battery gained 20 charge!");
    }
}
