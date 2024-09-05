using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Sphere_Controller : MonoBehaviour
{
    public float speed = 30f;
    public float maxTurnSpeed = 5f;
    public float minTurnSpeed = 1f;
    public float maxVelocityForTurnSpeed = 15f;
    public float gravityMultiplier = 30f;
    public float boostMultiplier = 1.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;

    [SerializeField] private EventReference upgradeSound;

    // Add references to the Trail Renderers
    public TrailRenderer leftWheelTrail;
    public TrailRenderer rightWheelTrail;

    // Add reference to the Dust Particle System
    public ParticleSystem dustParticleSystem;

    private bool isGrounded = false; // Track grounded state
    private bool isOnDustSurface = false; // Track if on dust surface

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure trail renderers are disabled at the start
        if (leftWheelTrail) leftWheelTrail.emitting = false;
        if (rightWheelTrail) rightWheelTrail.emitting = false;

        // Ensure dust particle system is not playing at the start
        if (dustParticleSystem) dustParticleSystem.Stop();
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        ApplyGravity();
        UpdateTrails(); // Update trail renderers based on grounded state
        UpdateDustEffect(); // Update dust effect based on grounded state and surface type
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
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
            {
                forceDirection *= boostMultiplier;
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

        float currentTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, rb.velocity.magnitude / maxVelocityForTurnSpeed);
        rb.AddTorque(Vector3.up * turn * currentTurnSpeed * 10f);
    }

    void ApplyGravity()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer);

        if (isGrounded)
        {
            Vector3 normal = hit.normal;
            Vector3 gravity = -normal * gravityMultiplier;
            rb.AddForce(gravity, ForceMode.Acceleration);

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f));

            // Check if the surface is either Sand1 or Sand2
            isOnDustSurface = (hit.collider.CompareTag("Sand1") || hit.collider.CompareTag("Sand2"));
        }
        else
        {
            rb.AddForce(Vector3.down * gravityMultiplier * 2f, ForceMode.Acceleration);
            isOnDustSurface = false; // Not on a dust surface if not grounded
        }
    }

    void UpdateTrails()
    {
        // Enable or disable trail renderers based on grounded state
        if (leftWheelTrail) leftWheelTrail.emitting = isGrounded;
        if (rightWheelTrail) rightWheelTrail.emitting = isGrounded;
    }

    void UpdateDustEffect()
    {
        // Enable or disable the dust particle effect based on grounded state and dust surface
        if (dustParticleSystem)
        {
            if (isGrounded && isOnDustSurface && rb.velocity.magnitude > 0.1f) // Adjust velocity threshold as needed
            {
                if (!dustParticleSystem.isEmitting)
                {
                    dustParticleSystem.Play();
                }
            }
            else
            {
                if (dustParticleSystem.isEmitting)
                {
                    dustParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                    // Allow particles to complete their natural lifetime
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoostUpgrade"))
        {
            boostMultiplier += 0.2f;
            Destroy(other.gameObject);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.upgradeSound, this.transform.position);
        }
    }
}
