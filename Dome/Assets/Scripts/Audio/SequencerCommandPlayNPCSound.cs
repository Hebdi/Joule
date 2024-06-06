using UnityEngine;
using PixelCrushers.DialogueSystem;
using FMODUnity;
using PixelCrushers.DialogueSystem.SequencerCommands;

public class SequencerCommandPlayNPCSound : SequencerCommand
{
    public void Start()
    {
        string npcName = GetParameter(0);
        DialogueSoundManager.Instance.PlayNPCSound(npcName);
        Stop();
    }
}
