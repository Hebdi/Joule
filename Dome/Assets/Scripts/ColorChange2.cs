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
        transitionCoroutine = StartCoroutine(FadeMaterial(0f)); // Fade to initial state
    }

    void OnDisable()
    {
        // Start fading to the second state (e.g., _Color2 and _EmissionColor2)
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(FadeMaterial(1f)); // Fade to alternate state
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
