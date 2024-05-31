using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class NPCActivation : MonoBehaviour
{
    [SerializeField] private EventReference NPCAwakeSound;

    void Update()
    {
            if (Input.GetKey(KeyCode.E))
            {
                AudioManager.instance.PlayOneShot(NPCAwakeSound, this.transform.position);
            }

    }

}
