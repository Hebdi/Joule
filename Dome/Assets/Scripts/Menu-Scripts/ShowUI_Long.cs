using System.Collections;
using UnityEngine;

public class ShowUI_Long : MonoBehaviour
{
    // Reference to the UI frame (parent GameObject)
    public GameObject uiFrame;

    // Reference to the text container under the UI frame
    public GameObject textContainer;

    // Index to specify which child text to display
    public int textToShowIndex;

    private void Start()
    {
        // Deactivate all text objects initially but keep the frame active
        SetAllTextChildrenActive(false);
        uiFrame.SetActive(false);  // Ensure the frame is initially disabled
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            // Activate the UI frame (ensuring backdrop, border, etc. remain visible)
            uiFrame.SetActive(true);

            // Deactivate all texts and then activate the specific one
            SetAllTextChildrenActive(false);
            SetSpecificTextActive(textToShowIndex, true);

            // Start the coroutine to hide UI after some time
            StartCoroutine(HideUIAfterDelay());
        }
    }

    private void SetAllTextChildrenActive(bool isActive)
    {
        // Ensure only the text objects under the text container are set active/inactive
        foreach (Transform child in textContainer.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    private void SetSpecificTextActive(int index, bool isActive)
    {
        // Check if the index is within bounds
        if (index >= 0 && index < textContainer.transform.childCount)
        {
            textContainer.transform.GetChild(index).gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("Text index is out of bounds.");
        }
    }

    private IEnumerator HideUIAfterDelay()
    {
        // Wait for 10 seconds before hiding the UI
        yield return new WaitForSeconds(10);

        // Deactivate all texts
        SetAllTextChildrenActive(false);

        // Deactivate the UI frame
        uiFrame.SetActive(false);

        Destroy(gameObject);  // Destroy the trigger object
    }
}
