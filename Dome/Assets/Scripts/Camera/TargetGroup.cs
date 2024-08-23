using UnityEngine;
using Cinemachine;

public class TargetGroup : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineTargetGroup targetGroup;
    public Transform player; // Reference to the player
    public string npcTag = "NPC"; // The tag used to identify NPCs
    public float updateInterval = 0.5f; // Time between target group updates

    private float timer;

    void Start()
    {
        if (virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        if (targetGroup == null)
        {
            Debug.LogError("No Cinemachine Target Group assigned!");
            return;
        }

        if (virtualCamera != null)
        {
            virtualCamera.LookAt = targetGroup.transform;
            virtualCamera.Follow = targetGroup.transform;
        }

        UpdateTargetGroup();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            UpdateTargetGroup();
            timer = 0f;
        }
    }

    void UpdateTargetGroup()
    {
        if (player == null) return;

        Transform nearestNPC = FindNearestNPC();

        // Clear the target group and add the player
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];

        // Add the player to the target group
        targetGroup.AddMember(player, 1f, 2f); // Weight and radius are adjustable

        // Add the nearest NPC to the target group if found
        if (nearestNPC != null)
        {
            targetGroup.AddMember(nearestNPC, 1f, 2f); // Weight and radius are adjustable
        }
    }

    Transform FindNearestNPC()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
        if (npcs.Length == 0) return null;

        Transform nearestNPC = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject npc in npcs)
        {
            float distance = Vector3.Distance(player.position, npc.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNPC = npc.transform;
            }
        }

        return nearestNPC;
    }
}
