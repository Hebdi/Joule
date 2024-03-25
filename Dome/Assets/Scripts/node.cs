using UnityEngine;

public class node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;



    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }


    void OnTriggerStay(Collider collider)
    {
        if ((collider.gameObject.tag != "Player") && (Input.GetKey(KeyCode.Space)))
        {
            Debug.Log("Can't Build here! No Pipes!");
            return;
        }

        if ((collider.gameObject.tag == "Player") && (Input.GetKey(KeyCode.Space)) && (turret != null))
        {
            Debug.Log("Can't Build here! There is already a turret!");
            return;
        }

        if ((collider.gameObject.tag == "Player") && (Input.GetKey(KeyCode.Space)))
        {
            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
         
        }
        if ((collider.gameObject.tag == "Player") && (turret == null))
        {
            rend.material.color = hoverColor;
        }

        if ((collider.gameObject.tag == "Player") && (turret != null))
        {
            rend.material.color = startColor;
        }
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            rend.material.color = startColor;
        }
    }

}