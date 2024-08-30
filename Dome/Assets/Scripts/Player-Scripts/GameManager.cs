using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;
    public float pauseDelay = 1f;  // Delay before pausing the game (adjust based on UI animation length)

    private PlayerStats playerStats; // Reference to the PlayerStats instance

    void Start()
    {
        GameIsOver = false;
        Time.timeScale = 1; // Ensure game time is normal at start

        // Find the PlayerStats instance in the scene
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats instance not found in the scene!");
        }
    }

    void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        // Check the health through the playerStats instance
        if (playerStats != null && playerStats.currentHealth <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true); // Show the Game Over UI
        StartCoroutine(PauseGameWithDelay()); // Start coroutine to pause the game after a delay
    }

    IEnumerator PauseGameWithDelay()
    {
        yield return new WaitForSecondsRealtime(pauseDelay); // Wait for the specified delay time using unscaled time
        PauseGame();  // Pause the game after the delay
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        Debug.Log("Game is paused after UI animation.");
    }

    public void ResumeGame() // If you want to resume or restart the game
    {
        Time.timeScale = 1; // Resume the game
        GameIsOver = false;
        gameOverUI.SetActive(false); // Hide the Game Over UI
    }
}
