using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class energyCollect : MonoBehaviour
{
    [SerializeField] private EventReference batteryCollectedSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlayOneShot(batteryCollectedSound, this.transform.position);
        }

    }

}
