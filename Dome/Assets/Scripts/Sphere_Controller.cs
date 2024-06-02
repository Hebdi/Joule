using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Sphere_Controller : MonoBehaviour
{
    // public GameObject dustTrail;

    public float speed;
    public float turnspeed;
    public float gravitymultiplier;
    //boost at 4 at the start, after upgrade 8 already enough
    public float boost;

    private Rigidbody rb;

    [SerializeField] private EventReference upgradeSound;
    //[SerializeField] private EventReference playerRollSound;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Turn();
        Fall();
    }

    void Move()
    {

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * 10);
                
            }


        if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(-(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * 10));
            //dustTrail.SetActive(true);
          // AudioManager.instance.PlayOneShot(FMODEvents.instance.playerRollSound, this.transform.position); //Play Roll Sound

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
                {
                    rb.AddRelativeForce(-(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * boost));
                }
            }
       // else
        {
           // dustTrail.SetActive(false);
        }
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            localVelocity.x = 0;
            rb.velocity = transform.TransformDirection(localVelocity);
    }
    void Turn()
    {
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(Vector3.up * -turnspeed * -10);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * turnspeed * 10);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(-Vector3.up * -turnspeed * -10);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(-Vector3.up * turnspeed * 10);
        }
    }
    void Fall()
    {
        rb.AddForce(Vector3.down * gravitymultiplier * 20);
    }

    // pick up power ups

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("BoostUpgrade"))
        {
           
            boost = boost + 2;
            Destroy(other.gameObject);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.upgradeSound, this.transform.position); //play Audio when entering upgrade collider
        }

    }


}
