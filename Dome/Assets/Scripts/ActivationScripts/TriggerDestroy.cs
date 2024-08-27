using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{

    //Der Dialog Trigger wird erst aktiv, wenn der Joule den Trigger auslöst
    public GameObject enableTargetObject;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }

}
