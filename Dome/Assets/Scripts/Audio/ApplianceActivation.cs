using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceActivation: MonoBehaviour
{


    void Update()
    {
            if (Input.GetKey(KeyCode.E))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.ApplianceAwakeSound, this.transform.position);
            }

    }

}
