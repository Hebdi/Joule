using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth;

    public HealthBar healthBar;
    public int energyUse = 100;
    public float boostHealthDrainPerSecond = 50f;

    private bool isBoosting;
    public Image healthImage;
    public Image blinkImage;
    public float blinkStartSpeed = 2.0f;
    public float blinkEndSpeed = 0.1f;

    private Coroutine blinkCoroutine;
    private FMOD.Studio.EventInstance lowBatteryEvent;
    public EventReference lowBatteryEventReference;
    private bool lowBatteryEventStarted = false;

    // New boolean to trigger energy consumption
    public bool consumeEnergy = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UpdateHealthImageAlpha();
        StartCoroutine(UpdateBlinkingSpeed());
    }

    void FixedUpdate()
    {
        // Handle boosting logic
        isBoosting = Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space));
        if (isBoosting && currentHealth > 0)
        {
            float drainAmount = boostHealthDrainPerSecond * Time.deltaTime;
            drainAmount = Mathf.Clamp(drainAmount, 0f, currentHealth);
            LoseHealth(Mathf.RoundToInt(drainAmount));
        }

        // Check if consumeEnergy is true and deduct energy if so
        if (consumeEnergy)
        {
            LoseHealth(energyUse); // Deduct 100 energy (or health)
            consumeEnergy = false; // Reset the flag to false
        }
    }

    void Update()
    {
        // Update blinking and FMOD parameters based on current health
        CheckHealthForBlinking();
        UpdateLowBatteryParameter();
    }

    public void LoseHealth(int damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthBar.SetHealth(currentHealth);
        UpdateHealthImageAlpha();

        if (currentHealth <= 0)
        {
            StopLowBatteryEvent();
            this.enabled = false;
        }
    }

    void GainHealth(int heal)
    {
        if (heal <= 0) return;

        currentHealth += heal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth);
        UpdateHealthImageAlpha();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyRefill"))
        {
            if (currentHealth < 800)
            {
                GainHealth(200);
            }
            else
            {
                currentHealth = maxHealth;
                healthBar.SetHealth(currentHealth);
                UpdateHealthImageAlpha();
            }

            Destroy(other.gameObject);
        }
    }


    void UpdateHealthImageAlpha()
    {
        float alpha = (float)currentHealth / maxHealth;
        Color color = healthImage.color;
        color.a = alpha;
        healthImage.color = color;
    }

    void CheckHealthForBlinking()
    {
        if (currentHealth < maxHealth * 0.3f)
        {
            if (!lowBatteryEventStarted)
            {
                lowBatteryEvent = FMODUnity.RuntimeManager.CreateInstance(lowBatteryEventReference);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(lowBatteryEvent, transform, GetComponent<Rigidbody>());
                lowBatteryEvent.start();
                lowBatteryEventStarted = true;
            }

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
                SetBlinkImageAlpha(0f);
            }
            if (lowBatteryEventStarted)
            {
                StopLowBatteryEvent();
            }
        }
    }

    IEnumerator UpdateBlinkingSpeed()
    {
        while (true)
        {
            if (currentHealth < maxHealth * 0.3f)
            {
                float blinkSpeed = Mathf.Lerp(blinkEndSpeed, blinkStartSpeed, (float)currentHealth / (maxHealth * 0.3f));
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                }
                blinkCoroutine = StartCoroutine(BlinkImage(blinkSpeed));
            }
            else
            {
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    SetBlinkImageAlpha(0f);
                }
            }
            yield return new WaitForSeconds(0.2f);
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

    void UpdateLowBatteryParameter()
    {
        if (lowBatteryEventStarted)
        {
            float batteryLevel = Mathf.InverseLerp(maxHealth * 0.3f, maxHealth * 0.1f, currentHealth);
            lowBatteryEvent.setParameterByName("Low Battery", batteryLevel);
        }
    }

    void StopLowBatteryEvent()
    {
        if (lowBatteryEventStarted)
        {
            lowBatteryEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            lowBatteryEventStarted = false;
        }
    }
}
