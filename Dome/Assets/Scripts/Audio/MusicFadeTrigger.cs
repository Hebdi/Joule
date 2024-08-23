using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class MusicFadeTrigger : MonoBehaviour
{
    [SerializeField] private EventReference musicEvent; // FMOD Event Reference for the music
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] private List<FadeOutZone> fadeOutZones; // List of fade out zones

    private EventInstance musicInstance;

    private void Start()
    {
        // Create an instance of the FMOD event and start playing it
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    private void Update()
    {
        if (playerTransform == null || fadeOutZones == null || fadeOutZones.Count == 0) return;

        // Calculate the volume based on the closest fade out zone
        float volume = 1.0f; // Start with full volume

        foreach (FadeOutZone zone in fadeOutZones)
        {
            // Calculate the distance between the player and the current fade out zone
            float distance = Vector3.Distance(playerTransform.position, zone.transform.position);

            // Adjust the volume based on the zone's radii and minimum volume
            volume = Mathf.Min(volume, CalculateVolume(distance, zone.minRadius, zone.maxRadius, zone.minVolume));
        }

        musicInstance.setVolume(volume);
    }

    private float CalculateVolume(float distance, float minRadius, float maxRadius, float minVolume)
    {
        if (distance >= maxRadius)
        {
            return 1.0f; // Full volume
        }
        else if (distance <= minRadius)
        {
            return minVolume; // Minimum volume based on the zone's setting
        }
        else
        {
            // Calculate the linear interpolation of volume between minRadius and maxRadius,
            // and then map it to the range between minVolume and 1.0 (full volume)
            float t = Mathf.InverseLerp(minRadius, maxRadius, distance);
            return Mathf.Lerp(minVolume, 1.0f, t);
        }
    }

    private void OnDestroy()
    {
        // Stop and release the FMOD event when the object is destroyed
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }
}

[System.Serializable]
public class FadeOutZone
{
    public Transform transform; // Position of the fade out zone
    public float minRadius = 5.0f; // Radius where the music is fully faded out
    public float maxRadius = 15.0f; // Radius where the music is at full volume
    public float minVolume = 0.0f; // Minimum volume level when within the minRadius
}
