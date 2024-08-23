using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicFadeTrigger : MonoBehaviour
{
    [SerializeField] private EventReference musicEvent; // FMOD Event Reference for the music
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] private float minRadius = 5.0f; // Radius where the music is fully faded out
    [SerializeField] private float maxRadius = 15.0f; // Radius where the music is at full volume

    private EventInstance musicInstance;

    private void Start()
    {
        // Create an instance of the FMOD event and start playing it
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // Calculate the distance between the player and the trigger center
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Calculate the volume based on the distance
        float volume = CalculateVolume(distance);
        musicInstance.setVolume(volume);
    }

    private float CalculateVolume(float distance)
    {
        if (distance >= maxRadius)
        {
            return 1.0f; // Full volume
        }
        else if (distance <= minRadius)
        {
            return 0.0f; // Fully attenuated (silent)
        }
        else
        {
            // Calculate the linear interpolation of volume between minRadius and maxRadius
            return Mathf.InverseLerp(minRadius, maxRadius, distance);
        }
    }

    private void OnDestroy()
    {
        // Stop and release the FMOD event when the object is destroyed
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }
}
