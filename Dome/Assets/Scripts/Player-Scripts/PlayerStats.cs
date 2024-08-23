using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image healthImage; // Reference to the image whose alpha will be adjusted
    public Image blinkImage; // Reference to the image that will blink
    public float blinkStartSpeed = 2.0f; // Slower blinking speed at 40% health
    public float blinkEndSpeed = 0.1f; // Faster blinking speed at 0% health

    private Coroutine blinkCoroutine;

    void Start()
    {
        Lives = startLives;
        Rounds = 0;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UpdateHealthImageAlpha();
        StartCoroutine(UpdateBlinkingSpeed());
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
        UpdateHealthImageAlpha();
        CheckHealthForBlinking();

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
        UpdateHealthImageAlpha();
        CheckHealthForBlinking();

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died");
            this.enabled = false;
        }
    }

    void GainHealth(int heal)
    {
        currentHealth += heal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth);
        UpdateHealthImageAlpha();
        CheckHealthForBlinking();
        Debug.Log("Your battery gained 20 charge!");
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
            UpdateHealthImageAlpha();
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

    void UpdateHealthImageAlpha()
    {
        // Calculate the alpha value based on current health
        float alpha = (float)currentHealth / maxHealth;
        Color color = healthImage.color;
        color.a = alpha;
        healthImage.color = color;
    }

    void CheckHealthForBlinking()
    {
        if (currentHealth < maxHealth * 0.3f)
        {
            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkImage(blinkStartSpeed));
            }
        }
        else
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                SetBlinkImageAlpha(0f); // Ensure the blink image is hidden when health is above 30%
            }
        }
    }

    IEnumerator UpdateBlinkingSpeed()
    {
        while (true)
        {
            if (currentHealth < maxHealth * 0.4f)
            {
                // Calculate new blink speed
                float blinkSpeed = Mathf.Lerp(blinkStartSpeed, blinkEndSpeed, (1 - (float)currentHealth / (maxHealth * 0.3f)));

                // Wait for the current blink cycle to complete
                yield return new WaitUntil(() => blinkCoroutine == null); // Wait until blinking is stopped

                blinkCoroutine = StartCoroutine(BlinkImage(blinkSpeed));
            }
            yield return new WaitForSeconds(0.2f); // Update every second
        }
    }

    IEnumerator BlinkImage(float blinkSpeed)
    {
        while (true)
        {
            float pingPong = Mathf.PingPong(Time.time * blinkSpeed, 2f);
            SetBlinkImageAlpha(pingPong);
            yield return null;
        }
    }

    void SetBlinkImageAlpha(float alpha)
    {
        Color color = blinkImage.color;
        color.a = alpha;
        blinkImage.color = color;
    }
}
