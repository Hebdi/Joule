using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOn : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            AudioManager.instance.PlayOneShot(FMODEvents.instance.lightOn, this.transform.position);
        }

    }

}
