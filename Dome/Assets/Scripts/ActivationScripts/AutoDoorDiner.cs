using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AutoDoorDiner : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private float closeDelay = 3.0f; // Time in seconds before the door closes
    [SerializeField] private EventReference doorOpenSound; // FMOD Event Reference for door open sound
    [SerializeField] private EventReference doorCloseSound; // FMOD Event Reference for door close sound

    private bool isOpen = false; // To keep track of door state
    private bool playerInsideTrigger = false; // To track if player is inside trigger zone
    private bool isPlayingOpenSound = false; // To ensure the open sound plays only once
    private bool isPlayingCloseSound = false; // To ensure the close sound plays only once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            if (!isOpen)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            if (isOpen)
            {
                StartCoroutine(CloseDoorAfterDelay());
            }
        }
    }

    private void OpenDoor()
    {
        myDoor.SetBool("IsOpen", true); // Trigger the Animator parameter to open the door

        // Only play the open sound if it hasn't been played yet
        if (!isPlayingOpenSound)
        {
            PlayDoorSound(doorOpenSound);
            isPlayingOpenSound = true; // Mark the open sound as played
            isPlayingCloseSound = false; // Reset the close sound flag
        }

        isOpen = true;
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        if (!playerInsideTrigger) // Check if player is still inside trigger before closing
        {
            myDoor.SetBool("IsOpen", false); // Trigger the Animator parameter to close the door

            // Only play the close sound if it hasn't been played yet
            if (!isPlayingCloseSound)
            {
                PlayDoorSound(doorCloseSound);
                isPlayingCloseSound = true; // Mark the close sound as played
                isPlayingOpenSound = false; // Reset the open sound flag
            }

            isOpen = false;
        }
    }

    private void PlayDoorSound(EventReference soundEvent)
    {
        RuntimeManager.PlayOneShot(soundEvent, transform.position);
    }
}
