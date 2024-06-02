using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
   [field: Header("Player Roll SFX")]
   [field: SerializeField] public EventReference playerRollSound { get; private set; }

    [field: Header("Battery SFX")]
    [field: SerializeField] public EventReference energyCollect { get; private set; }

    [field: Header("Monument Activation SFX")]
    [field: SerializeField] public EventReference MonumentAwakeSound { get; private set; }

    [field: Header("Light SFX")]
    [field: SerializeField] public EventReference lightOn { get; private set; }

    [field: Header("ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("NPC SFX")]
    [field: SerializeField] public EventReference NPCAwakeSound { get; private set; }

    [field: Header("Upgrade SFX")]
    [field: SerializeField] public EventReference upgradeSound { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events script in the scene.");
        }
        instance = this;
    }

}
