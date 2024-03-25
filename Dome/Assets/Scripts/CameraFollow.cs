using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float rotsmoothing;
    public float smoothing;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotsmoothing);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y-1, 0));

        if (Input.GetKey(KeyCode.E))
        {
            transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotsmoothing);
            transform.rotation = Quaternion.Euler(new Vector3(16, transform.rotation.eulerAngles.y-1, 0));

        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotsmoothing);
            transform.rotation = Quaternion.Euler(new Vector3(-16, transform.rotation.eulerAngles.y - 1, 0));

        }
    }
}
