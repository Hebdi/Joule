using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivation_vaultKey : MonoBehaviour
{
    // The object to enable when the trigger is activated
    public GameObject enableTargetObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VaultKey"))
        {
            enableTargetObject.SetActive(true);
            Destroy(this); // Destroy this script component
        }
    }
}
