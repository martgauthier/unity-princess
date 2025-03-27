using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class IceFadeOut : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 4f;
    public float fadeDuration = 3f;

    public Renderer iceRenderer; // Renderer avec le matériau "statue-ice"

    private bool hasFaded = false;
    private Material fadeMaterial;

    void Start()
    {
        // Cloner le matériau pour éviter d'affecter tous les objets partageant le même
        fadeMaterial = new Material(iceRenderer.material);
        iceRenderer.material = fadeMaterial;

        // S'assurer qu'il est bien opaque au départ
        Color baseColor = fadeMaterial.color;
        baseColor.a = 1f;
        fadeMaterial.color = baseColor;

        // Configurer le mode de rendu transparent
        fadeMaterial.SetFloat("_Mode", 3); // Transparent
        fadeMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        fadeMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        fadeMaterial.SetInt("_ZWrite", 0);
        fadeMaterial.DisableKeyword("_ALPHATEST_ON");
        fadeMaterial.EnableKeyword("_ALPHABLEND_ON");
        fadeMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        fadeMaterial.renderQueue = 3000;
    }

    void Update()
    {
        if (hasFaded) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= activationDistance)
        {
            StartCoroutine(FadeOut());
            hasFaded = true;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color color = fadeMaterial.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(1f, 0f, t);
            fadeMaterial.color = color;
            yield return null;
        }

        // Optionnel : désactiver la statue glacée une fois invisible
        gameObject.SetActive(false);
    }
}

