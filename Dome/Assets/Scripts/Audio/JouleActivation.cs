using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JouleActivation: MonoBehaviour
{


    void Update()
    {
            if (Input.GetKey(KeyCode.E))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.JouleAwakeSound, this.transform.position);
            }

    }

}
