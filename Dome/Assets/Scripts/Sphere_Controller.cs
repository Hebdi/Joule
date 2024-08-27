using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Sphere_Controller : MonoBehaviour
{
    public float speed = 30f;
    public float maxTurnSpeed = 5f; // Maximum turn speed when stationary
    public float minTurnSpeed = 1f; // Minimum turn speed when moving
    public float maxVelocityForTurnSpeed = 15f; // Velocity considered for max turn speed
    public float gravityMultiplier = 30f;
    public float boostMultiplier = 1.2f; // Reduced boost multiplier
    public LayerMask groundLayer;

    private Rigidbody rb;

    [SerializeField] private EventReference upgradeSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        ApplyGravity();
    }

    void Move()
    {
        Vector3 forceDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.S))
        {
            forceDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.W))
        {
            forceDirection -= Vector3.forward;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                forceDirection *= boostMultiplier; // Adjusted boost multiplier
            }
        }

        forceDirection = transform.TransformDirection(forceDirection) * speed;
        rb.AddForce(forceDirection);

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x = 0;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    void Turn()
    {
        float turn = 0f;
        if (Input.GetKey(KeyCode.D))
        {
            turn = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            turn = -1f;
        }

        // Calculate turn speed based on current velocity
        float currentTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, rb.velocity.magnitude / maxVelocityForTurnSpeed);
        rb.AddTorque(Vector3.up * turn * currentTurnSpeed * 10f);
    }

    void ApplyGravity()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer);

        if (isGrounded)
        {
            Vector3 normal = hit.normal;
            Vector3 gravity = -normal * gravityMultiplier;
            rb.AddForce(gravity, ForceMode.Acceleration);

            // Apply torque to keep the robot's upright position
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }
        else
        {
            rb.AddForce(Vector3.down * gravityMultiplier * 2f, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoostUpgrade"))
        {
            boostMultiplier += 0.2f; // Reduced boost increase
            Destroy(other.gameObject);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.upgradeSound, this.transform.position);
        }
    }
}
