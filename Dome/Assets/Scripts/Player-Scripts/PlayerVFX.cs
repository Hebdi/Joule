using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerVFX : MonoBehaviour
{
    [System.Serializable]
    public class EffectSettings
    {
        public string triggerTag;  // The tag of the trigger zone to detect
        public GameObject effectPrefab;  // The visual effect to play

        // Three FMOD events to choose from
        public EventReference soundEvent1;
        public float soundDelay1 = 0.5f;  // Delay before playing soundEvent1

        public EventReference soundEvent2;
        public float soundDelay2 = 1.0f;  // Delay before playing soundEvent2

        public EventReference soundEvent3;
        public float soundDelay3 = 1.5f;  // Delay before playing soundEvent3
    }

    public List<EffectSettings> effects;  // List of different effects to handle

    private void OnTriggerEnter(Collider other)
    {
        foreach (EffectSettings effectSetting in effects)
        {
            // Check if the player enters a trigger zone with the correct tag
            if (other.CompareTag(effectSetting.triggerTag))
            {
                // Activate the visual effect as a child of the player
                if (effectSetting.effectPrefab != null)
                {
                    GameObject effectInstance = Instantiate(effectSetting.effectPrefab, transform.position, Quaternion.identity, transform);
                    Destroy(effectInstance, 6f); // Destroy the effect after 6 seconds, or adjust as needed
                }

                // Play the FMOD sound events after their respective delays
                if (effectSetting.soundEvent1.IsNull == false)
                {
                    StartCoroutine(PlaySoundWithDelay(effectSetting.soundEvent1, effectSetting.soundDelay1));
                }
                if (effectSetting.soundEvent2.IsNull == false)
                {
                    StartCoroutine(PlaySoundWithDelay(effectSetting.soundEvent2, effectSetting.soundDelay2));
                }
                if (effectSetting.soundEvent3.IsNull == false)
                {
                    StartCoroutine(PlaySoundWithDelay(effectSetting.soundEvent3, effectSetting.soundDelay3));
                }

                break; // Exit the loop after processing the first matching effect
            }
        }
    }

    private IEnumerator PlaySoundWithDelay(EventReference soundEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        RuntimeManager.PlayOneShot(soundEvent, transform.position);
    }
}
