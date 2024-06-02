using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyCollect : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlayOneShot(FMODEvents.instance.energyCollect, this.transform.position);
        }

    }

}
