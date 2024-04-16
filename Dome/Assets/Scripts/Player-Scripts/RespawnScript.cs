using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{


    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(-1581.469970703125f, 90.11299896240235f, -1186.8599853515625f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OffMap"))
        {

            transform.position = new Vector3(-2809.263916015625f, 90.97000122070313f, -2606.365966796875f);

        }
    }
}
//UnityEditor.TransformWorldPlacementJSON:{ "position":{ "x":-955.2474365234375,"y":0.01759999990463257,"z":-865.030517578125},"rotation":{ "x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{ "x":1.0,"y":1.0,"z":1.0} } Behind Hill
//UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-1581.469970703125,"y":90.11299896240235,"z":-1186.8599853515625},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}} On DinerHill
//UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-2809.263916015625,"y":90.97000122070313,"z":-2606.365966796875},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}} On CanyonHill