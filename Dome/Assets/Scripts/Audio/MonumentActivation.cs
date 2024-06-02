using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentActivation : MonoBehaviour
{


    void Update()
    {

        if (Input.GetKey(KeyCode.E))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.MonumentAwakeSound, this.transform.position);
            }

    }

}
