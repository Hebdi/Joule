using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorLeft: MonoBehaviour
{
    public Animator doorLeftAnim;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DoorTrigger"))
        {
            doorLeftAnim.SetTrigger("Door-Open-left");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorTrigger"))
        {
            doorLeftAnim.SetTrigger("Door-Close-left");
        }
    }
}
