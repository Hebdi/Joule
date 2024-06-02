using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckAudio : MonoBehaviour
{

    public bool grounded = false;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f; //slightly above 0



    // Update is called once per frame
    void Update()
    {
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferCheckDistance;
        if (Input.GetKeyDown(KeyCode.W)&&grounded) //roll forwards

        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerRollSound, this.transform.position);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            grounded = true;
        } else
        { //ray did not hit ground
            grounded = false;
        }

    }

}
