using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustTrail : MonoBehaviour
{
    public ParticleSystem dust;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            dust.Play();
        }
    }
}
