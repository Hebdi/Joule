using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOn : MonoBehaviour
{

    //Der Dialog Trigger wird erst aktiv, wenn der Joule den Trigger auslöst
    public GameObject enableTargetLight;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            enableTargetLight.gameObject.SetActive(true);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.lightOn, this.transform.position);
            Destroy(gameObject);
        }
    }
}
