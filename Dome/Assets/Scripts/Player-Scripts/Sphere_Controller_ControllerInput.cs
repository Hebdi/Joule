using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.InputSystem;

public class Sphere_Controller_ControllerInput : MonoBehaviour
{
    public float speed = 30f;
    public float turnSpeed = 2f; // Adjust sensitivity for turning
    public float gravityMultiplier = 30f;
    public float boostMultiplier = 1.2f;
    public LayerMask groundLayer;

    public Cinemachine.CinemachineFreeLook thirdPersonCamera; // Reference to the Cinemachine camera

    private Rigidbody rb;
    private ControllerInput controls;
    private bool isBoosting = false;

    [SerializeField] private EventReference upgradeSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new ControllerInput();

        // Input actions
        controls.PlayerControls.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.PlayerControls.LookAround.performed += ctx => LookAround(ctx.ReadValue<Vector2>()); // Adjusted action name
        controls.PlayerControls.Boost.performed += ctx => StartBoost();
        controls.PlayerControls.Boost.canceled += ctx => StopBoost();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    void Move(Vector2 input)
    {
        Vector3 forceDirection = Vector3.zero;

        // Handle forward/backward movement
        if (input.y > 0) // Move forward
        {
            forceDirection += Vector3.forward;
        }
        else if (input.y < 0) // Move backward
        {
            forceDirection -= Vector3.forward;
        }

        // Boosting logic
        if (isBoosting)
        {
            forceDirection *= boostMultiplier;
        }

        forceDirection = transform.TransformDirection(forceDirection) * speed;
        rb.AddForce(forceDirection);

        // Handle turning slowly towards camera direction when moving
        if (input.y != 0) // Only turn when moving
        {
            float targetYRotation = thirdPersonCamera.transform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void LookAround(Vector2 lookInput)
    {
        // Rotate the camera based on right stick input
        float lookX = lookInput.x * 0.1f; // Adjust sensitivity here
        float lookY = lookInput.y * 0.1f; // Adjust sensitivity here

        // Assuming the camera uses the Cinemachine FreeLook
        thirdPersonCamera.m_Orbits[1].m_Height += lookY; // Adjusts height
        thirdPersonCamera.m_Orbits[1].m_Radius += lookX; // Adjusts distance (radius)
    }

    void StartBoost()
    {
        isBoosting = true;
    }

    void StopBoost()
    {
        isBoosting = false;
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

            // Apply torque to keep the robot upright
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
