using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckAudio : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public string[] terrainTypeArray = new string[] { "Sand1", "Sand2", "Concrete", "Monument" };

    float terrainTypeFloat = 1f;  // Initialize to 1 to match FMOD parameter range
    float speed = 0f;
    bool isBoosting = false;

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
        // Set initial 3D attributes
        rollOnGround.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        // Start the sound
        rollOnGround.start();
    }

    void Update()
    {
        // Check if the player is grounded
        bool isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        if (isGrounded)
        {
            // Cast a ray downwards to detect the ground type
            RaycastHit hit;
            if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance, groundMask))
            {
                // Determine the terrain type based on the tag
                string groundTag = hit.collider.tag;
                terrainTypeFloat = GetTerrainTypeFloat(groundTag);

                // Set the global terrain type parameter
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TerrainType", terrainTypeFloat);
            }

            // Check if the player is moving forward or backward
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                // Gradually increase the speed parameter
                if (!isBoosting)
                    speed = Mathf.MoveTowards(speed, 0.8f, Time.deltaTime * 2f); // Adjust the multiplier for desired acceleration
                else
                    speed = Mathf.MoveTowards(speed, 1f, Time.deltaTime * 2f); // Adjust the multiplier for desired acceleration during boost
            }
            else
            {
                // Gradually decrease the speed parameter
                speed = Mathf.MoveTowards(speed, 0f, Time.deltaTime * 2f); // Adjust the multiplier for desired deceleration
            }

            // Check for boost activation
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isBoosting = true;
            }

            // Check for boost deactivation
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isBoosting = false;
            }

            // Set the speed parameter in FMOD
            rollOnGround.setParameterByName("Speed", speed);

            // Set 3D attributes
            rollOnGround.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

            // Start the sound if it's not already playing
            FMOD.Studio.PLAYBACK_STATE playbackState;
            rollOnGround.getPlaybackState(out playbackState);
            if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                rollOnGround.start();
            }
        }
        else
        {
            // Stop the sound if not grounded
            rollOnGround.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            speed = 0f;
        }
    }

    // Function to convert terrain type tags to corresponding float values for FMOD
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
