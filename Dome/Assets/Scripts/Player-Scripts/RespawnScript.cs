using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{


    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.G))
        {
            transform.position = new Vector3(-4484.958984375f, 14f, -2720.81591796875f);
        }
        if (Input.GetKey(KeyCode.H))
        {
            transform.position = new Vector3(-4834.35009765625f, 64.52999877929688f, -2081.090087890625f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OffMap"))
        {

            transform.position = new Vector3(-4484.958984375f, 14f, -2720.81591796875f);

        }
    }
}
//Infront of Diner: UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-4834.35009765625,"y":64.52999877929688,"z":-2081.090087890625},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}}
//Infront of Bridge: UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-4484.958984375,"y":14.0,"z":-2720.81591796875},"rotation":{"x":0.0,"y":0.9975530505180359,"z":0.0,"w":0.0699143037199974},"scale":{"x":7.0,"y":7.0,"z":7.0}}
//UnityEditor.TransformWorldPlacementJSON:{ "position":{ "x":-955.2474365234375,"y":0.01759999990463257,"z":-865.030517578125},"rotation":{ "x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{ "x":1.0,"y":1.0,"z":1.0} } Behind Hill
//UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-1581.469970703125,"y":90.11299896240235,"z":-1186.8599853515625},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}} On DinerHill
//UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-2809.263916015625,"y":90.97000122070313,"z":-2606.365966796875},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}} On CanyonHill
//UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-3739.86865234375,"y":14.270000457763672,"z":-1485.2315673828125},"rotation":{"x":0.0,"y":0.9975530505180359,"z":0.0,"w":0.0699143037199974},"scale":{"x":1.0,"y":1.0,"z":1.0}} At Canyon - Playtesting Area
//UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-4833.9599609375,"y":103.46600341796875,"z":-1806.7900390625},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}} On Hill - Playtesting Area