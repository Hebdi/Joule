using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivation : MonoBehaviour
{

    //Der Dialog Trigger wird erst aktiv, wenn der Joule den Trigger auslöst
    public GameObject enableTargetObject;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            enableTargetObject.gameObject.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            print("trigger left");

            enableTargetObject.gameObject.SetActive(false);

        }
    }
}
