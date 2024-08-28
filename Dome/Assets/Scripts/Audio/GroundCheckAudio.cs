using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GroundCheckAudio : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistance = 1.8f;  // Increased distance for earlier ground detection
    public LayerMask groundMask;
    public string[] terrainTypeArray = new string[] { "Sand1", "Sand2", "Concrete", "Monument" };

    public EventReference landingSoundEvent;

    float terrainTypeFloat = 1f;  // Initialize to 1 to match FMOD parameter range
    float speed = 0f;
    bool isBoosting = false;
    bool wasGrounded = true;
    bool landingSoundPlayed = false;

    FMOD.Studio.EventInstance rollOnGround;

    void Awake()
    {
        // Create the FMOD event instance for the rolling sound
        rollOnGround = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Terrains");
    }

    void Start()
    {
        // Attach the FMOD instance to the GameObject
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(rollOnGround, transform, GetComponent<Rigidbody>());
        rollOnGround.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        rollOnGround.start();
    }

    void Update()
    {
        // Check if the player is grounded
        bool isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        if (isGrounded)
        {
            if (!wasGrounded && !landingSoundPlayed)
            {
                PlayLandingSound();
            }

            HandleGroundedAudio();
            wasGrounded = true;
            landingSoundPlayed = false; // Reset for the next jump
        }
        else
        {
            HandleAirborneAudio();
            wasGrounded = false;
        }

        UpdateSpeed();
    }

    void HandleGroundedAudio()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance, groundMask))
        {
            string groundTag = hit.collider.tag;
            terrainTypeFloat = GetTerrainTypeFloat(groundTag);

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TerrainType", terrainTypeFloat);
            Debug.Log($"Setting TerrainType to {terrainTypeFloat}");
        }
    }

    void HandleAirborneAudio()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TerrainType", 5f);
        Debug.Log("Setting TerrainType to 5f for airborne sound");
    }

    void UpdateSpeed()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (!isBoosting)
                speed = Mathf.MoveTowards(speed, 0.8f, Time.deltaTime * 2f);
            else
                speed = Mathf.MoveTowards(speed, 1f, Time.deltaTime * 2f);
        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0f, Time.deltaTime * 2f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
        {
            isBoosting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
        {
            isBoosting = false;
        }

        rollOnGround.setParameterByName("Speed", speed);
        Debug.Log($"Setting Speed parameter to {speed}");

        rollOnGround.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
    }

    void PlayLandingSound()
    {
        Debug.Log("Playing landing sound: " + landingSoundEvent);
        FMODUnity.RuntimeManager.PlayOneShot(landingSoundEvent, transform.position);
        landingSoundPlayed = true;
    }

    float GetTerrainTypeFloat(string groundTag)
    {
        switch (groundTag)
        {
            case "Sand1":
                return 1f;
            case "Sand2":
                return 2f;
            case "Concrete":
                return 3f;
            case "Monument":
                return 4f;
            default:
                return 1f; // Default to 1 to match FMOD parameter range
        }
    }
}
