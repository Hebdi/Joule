using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using FMODUnity;

public class DialogueSoundManager : MonoBehaviour
{
    public static DialogueSoundManager Instance { get; private set; }

    [SerializeField]
    private EventReference computerTalkSound; // FMOD event reference for Computer talk sound
    [SerializeField]
    private EventReference appliancesTalkSound; // FMOD event reference for Appliances talk sound
    [SerializeField]
    private EventReference joulesTalkSound; // FMOD event reference for Joules talk sound

    private Transform playerTransform;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void PlayNPCSound(string npcName)
    {
        EventReference soundToPlay = default;

        switch (npcName)
        {
            case "Computer":
                soundToPlay = computerTalkSound;
                break;
            case "Appliances":
                soundToPlay = appliancesTalkSound;
                break;
            case "Joules":
                soundToPlay = joulesTalkSound;
                break;
            default:
                Debug.LogWarning("Unknown NPC name: " + npcName);
                return;
        }

        if (soundToPlay.IsNull)
        {
            Debug.LogWarning("Sound not assigned for NPC: " + npcName);
            return;
        }

        // Create an instance of the event
        var instance = RuntimeManager.CreateInstance(soundToPlay);

        // Set 3D attributes
        var attributes = RuntimeUtils.To3DAttributes(playerTransform.position);
        instance.set3DAttributes(attributes);

        // Start the event
        instance.start();

        // Release the instance once it is done
        instance.release();
    }
}
