using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorRight: MonoBehaviour
{
    public Animator doorRightAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorTrigger"))
        {
            doorRightAnim.SetTrigger("Door-Open-right");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorTrigger"))
        {
            doorRightAnim.SetTrigger("Door-Close-right");
        }
    }
}
