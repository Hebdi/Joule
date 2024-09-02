using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteBlinker : MonoBehaviour
{
    public Image targetImage; // The UI Image component to change the sprite for
    public Sprite[] sprites; // Array of sprites to cycle through
    public float minInterval = 0.5f; // Minimum time between sprite changes
    public float maxInterval = 2.0f; // Maximum time between sprite changes

    private void Start()
    {
        if (targetImage == null)
        {
            Debug.LogError("SpriteBlinker: Target Image is not set.");
            return;
        }

        if (sprites.Length == 0)
        {
            Debug.LogError("SpriteBlinker: Sprites array is empty.");
            return;
        }

        StartCoroutine(BlinkSprites());
    }

    private IEnumerator BlinkSprites()
    {
        while (true)
        {
            // Change to a random sprite
            int randomIndex = Random.Range(0, sprites.Length);
            if (sprites.Length > 0)
            {
                targetImage.sprite = sprites[randomIndex];
                Debug.Log($"Sprite changed to: {sprites[randomIndex].name}");
            }
            else
            {
                Debug.LogWarning("SpriteBlinker: No sprites to change.");
            }

            // Wait for a random interval between changes
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
