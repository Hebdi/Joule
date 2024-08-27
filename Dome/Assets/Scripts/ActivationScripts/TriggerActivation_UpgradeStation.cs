using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivation_UpgradeStation : MonoBehaviour
{

    //Der Dialog Trigger wird erst aktiv, wenn der Joule den Trigger auslöst
    public GameObject enableTargetObject;
    public GameObject disableTargetObject;
    bool enter; // boolean to determine whether you are inside the trigger or outside

    void Start()
    {
        enter = false; // before entering the trigger area
    }

    // Update is called once per frame
    void Update()
    {
        if (enter == true && Input.GetKeyDown(KeyCode.E)) // determine that only after the trigger is
        {                                                   // true and you press space you can enter the house
            enableTargetObject.gameObject.SetActive(true);
            disableTargetObject.gameObject.SetActive(false); 
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player")) //entering the trigger/collider attached to the
                                                     // Player. the house object must have tag named "house" 
        {                                           // with both of these enter will only be true when you are 
            enter = true;                           // inside the trigger
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player")) //exiting the trigger/collider attached to the 
        {                                               // house cause enter to become false
            enter = false;
        }
    }

}
