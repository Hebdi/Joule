using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckAudio : MonoBehaviour
{

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Input.GetKey(KeyCode.W))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerRollSound, this.transform.position); //Play Roll Sound
        }
    }

}
