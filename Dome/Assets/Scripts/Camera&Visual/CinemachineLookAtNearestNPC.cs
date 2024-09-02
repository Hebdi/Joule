using System.Linq;
using UnityEngine;
using Cinemachine;

public class CinemachineLookAtNearestNPC : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    public string npcTag = "NPC"; // The tag used to identify NPCs

    void Start()
    {
        if (virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        if (virtualCamera != null)
        {
            Transform nearestNPC = FindNearestNPC();
            if (nearestNPC != null)
            {
                virtualCamera.LookAt = nearestNPC;
            }
        }
    }

    void Update()
    {
        if (virtualCamera != null)
        {
            Transform nearestNPC = FindNearestNPC();
            if (nearestNPC != null)
            {
                virtualCamera.LookAt = nearestNPC;
            }
        }
    }

    Transform FindNearestNPC()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
        if (npcs.Length == 0) return null;

        Transform nearestNPC = npcs.OrderBy(npc => Vector3.Distance(transform.position, npc.transform.position)).First().transform;

        return nearestNPC;
    }
}
