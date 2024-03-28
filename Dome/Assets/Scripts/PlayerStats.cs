using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 20;
    public static int Rounds;

    //indicates how much Power the player has at the start of the game
    public int maxHealth = 100;

    public static int currentHealth;
    public HealthBar healthBar;

    //How much Energy the Joule needs to use an ability or power a device
    public int energyUse = 10;

    bool enter; // boolean to determine whether you are inside the trigger or outside




    void Start()
    {
        Lives = startLives;

        Rounds = 0;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LoseHealth(energyUse);
        }

        if (enter == true && Input.GetKeyDown(KeyCode.E)) // determine that only after the trigger is
        {                                                   // true and you press space you can enter the house
            LoseHealth(energyUse);
        }
    }



    // Lose Health accoding to the value set as damage

    void LoseHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyRefill") && PlayerStats.currentHealth < 80)
        {
            GainHealth(20);
            Destroy(other.gameObject);

        }

        else if (other.CompareTag("EnergyRefill") && PlayerStats.currentHealth >= 80)
        {
            currentHealth = 100;
            Destroy(other.gameObject);
            Debug.Log("You are fully charged!");
            healthBar.SetHealth(currentHealth);
        }

        if (GetComponent<Collider>().gameObject.CompareTag("NPC")) //entering the trigger/collider attached to the
                                                      // Player. the NPC object must have tag named "NPC" 
        {                                           // with both of these enter will only be true when you are 
            enter = true;                           // inside the trigger
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("NPC")) //exiting the trigger/collider attached to the 
        {                                               // house cause enter to become false
            enter = false;
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
