using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class batteryCollect : MonoBehaviour
{
    [SerializeField] private EventReference coinCollectedSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyRefill") || other.CompareTag("BoostUpgrade"))
        {
            StartCoroutine(PlaySoundWithDelayCoroutine(0.5f));
        }
    }

    IEnumerator PlaySoundWithDelayCoroutine(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the FMOD event
        AudioManager.instance.PlayOneShot(coinCollectedSound, this.transform.position);
    }
}
