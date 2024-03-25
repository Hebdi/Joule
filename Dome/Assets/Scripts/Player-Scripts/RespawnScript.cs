using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{


    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(23.22f, 1.09f, 27.45f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OffMap"))
        {

            transform.position = new Vector3(23.22f, 1.09f, 27.45f);

        }
    }
}
