using System.Collections;
using UnityEngine;

public class ColorChange2 : MonoBehaviour
{
    public Material BlendMaterial;      // The material using the custom shader
    public GameObject Object;           // The object whose material you want to change
    public float transitionDuration = 1.0f; // Duration of the transition in seconds

    private Coroutine transitionCoroutine;

    void Start()
    {
        // Ensure the object and blend material are assigned
        if (Object != null && BlendMaterial != null)
        {
            // Assign the BlendMaterial to the object
            Object.GetComponent<MeshRenderer>().material = BlendMaterial;

            // Start with the initial state
            BlendMaterial.SetFloat("_BlendFactor", 0f);
        }
    }

    void OnEnable()
    {
        // Start fading to the first state (e.g., _Color1 and _EmissionColor1)
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);

        // Only start the coroutine if the GameObject is active
        if (gameObject.activeInHierarchy)
        {
            transitionCoroutine = StartCoroutine(FadeMaterial(0f)); // Fade to initial state
        }
    }

    void OnDisable()
    {
        // Only start the coroutine if the GameObject is active
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);

        // Only start the coroutine if the GameObject is active (though in OnDisable, it typically is not)
        if (gameObject.activeInHierarchy)
        {
            transitionCoroutine = StartCoroutine(FadeMaterial(1f)); // Fade to alternate state
        }
        else
        {
            // Directly set the material property if the object is inactive
            BlendMaterial.SetFloat("_BlendFactor", 1f);
        }
    }

    IEnumerator FadeMaterial(float targetBlendFactor)
    {
        float startBlendFactor = BlendMaterial.GetFloat("_BlendFactor");
        float time = 0f;

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            float t = time / transitionDuration;

            float blendFactor = Mathf.Lerp(startBlendFactor, targetBlendFactor, t);
            BlendMaterial.SetFloat("_BlendFactor", blendFactor);

            yield return null;
        }

        BlendMaterial.SetFloat("_BlendFactor", targetBlendFactor); // Ensure final value
    }
}
