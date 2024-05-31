using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class batteryCollect : MonoBehaviour
{
    [SerializeField] private EventReference coinCollectedSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlayOneShot(coinCollectedSound, this.transform.position);
        }

    }

}
