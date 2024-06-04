using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerActivation : MonoBehaviour
{


    void Update()
    {
            if (Input.GetKey(KeyCode.E))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.ComputerAwakeSound, this.transform.position);
            }

    }

}
