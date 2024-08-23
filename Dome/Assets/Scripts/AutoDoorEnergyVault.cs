using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AutoDoorEnergyVault : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private float closeDelay = 3.0f; // Time in seconds before the door closes
    [SerializeField] private EventReference doorOpenSound; // FMOD Event Reference for door open sound
    [SerializeField] private EventReference doorCloseSound; // FMOD Event Reference for door close sound

    private bool isOpen = false; // To keep track of door state
    private bool playerInsideTrigger = false; // To track if player is inside trigger zone

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
        myDoor.Play("EnergyVaultDoor-open", 0, 0.0f);
        PlayDoorSound(doorOpenSound);
        isOpen = true;
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        if (!playerInsideTrigger) // Check if player is still inside trigger before closing
        {
            myDoor.Play("EnergyVaultDoor-close", 0, 0.0f);
            PlayDoorSound(doorCloseSound);
            isOpen = false;
        }
    }

    private void PlayDoorSound(EventReference soundEvent)
    {
        RuntimeManager.PlayOneShot(soundEvent, transform.position);
    }
}
