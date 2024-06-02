using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCActivation : MonoBehaviour
{


    void Update()
    {
            if (Input.GetKey(KeyCode.E))
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.NPCAwakeSound, this.transform.position);
            }

    }

}
