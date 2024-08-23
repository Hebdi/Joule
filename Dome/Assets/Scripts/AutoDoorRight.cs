using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; // Import FMOD

public class AutoDoorRight : MonoBehaviour
{
    [SerializeField] private Animator doorRightAnim = null; // Door animator
    [SerializeField] private EventReference doorOpenSound; // FMOD Event Reference for door open sound
    [SerializeField] private EventReference doorCloseSound; // FMOD Event Reference for door close sound

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorTrigger"))
        {
            doorRightAnim.SetTrigger("Door-Open-right");
            PlayDoorSound(doorOpenSound); // Play door open sound
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorTrigger"))
        {
            doorRightAnim.SetTrigger("Door-Close-right");
            PlayDoorSound(doorCloseSound); // Play door close sound
        }
    }

    private void PlayDoorSound(EventReference soundEvent)
    {
        RuntimeManager.PlayOneShot(soundEvent, transform.position); // Play the sound at the door's position
    }
}
