using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class OutlineEffect : MonoBehaviour
{
    public Color outlineColor = Color.black; // Outline color

    // Allow finer control over the outline thickness
    [Range(0.0001f, 0.1f)]
    public float outlineThickness = 0.0002f; // Default thickness set to 0.0002

    private GameObject outlineObject; // Object for the outline
    private Material outlineMaterial; // Material for the outline

    void Start()
    {
        // Find the custom outline shader
        Shader outlineShader = Shader.Find("Custom/OutlineOnly");
        if (outlineShader == null)
        {
            Debug.LogError("Outline shader not found!");
            return;
        }

        // Create a new material with the outline shader
        outlineMaterial = new Material(outlineShader);
        outlineMaterial.SetColor("_OutlineColor", outlineColor);
        outlineMaterial.SetFloat("_OutlineThickness", outlineThickness);

        CreateOutlineObject();
    }

    void CreateOutlineObject()
    {
        // Create the outline object as a duplicate of the original
        outlineObject = new GameObject(gameObject.name + "_Outline");
        outlineObject.transform.SetParent(transform, false);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;
        outlineObject.transform.localScale = Vector3.one;

        // Add the mesh components
        MeshFilter meshFilter = outlineObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = GetComponent<MeshFilter>().sharedMesh;

        MeshRenderer meshRenderer = outlineObject.AddComponent<MeshRenderer>();
        meshRenderer.material = outlineMaterial;

        // Set the rendering options
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;
    }

    void OnDestroy()
    {
        // Clean up the outline object
        if (outlineObject != null)
        {
            Destroy(outlineObject);
        }
    }
}
