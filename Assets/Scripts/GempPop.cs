using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GemPop : MonoBehaviour
{
    private Vector3 startScale = new Vector3(0.4f, 0.4f, 0.4f); // Grand au début
    private Vector3 targetScale = new Vector3(0.15f, 0.15f, 0.15f); // Petit à la fin
    private Vector3 startPositionOffset = new Vector3(0f, 0.5f, 0f); // Apparition plus haut
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float delay = 1f; // 1 secondes d'attente
    private float animationDuration = 1.5f; // Animation de descente
    private float elapsedTime = 0f;
    private bool hasAppeared = false;
    private bool isAnimating = false;

    private void Start()
    {
        transform.localScale = Vector3.zero; 
    }

    private void Update()
    {
        if (!hasAppeared)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {
                Appear();
            }
            return;
        }

        if (isAnimating)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            // Descente + réduction
            transform.localScale = Vector3.Lerp(startScale, targetScale, EaseOutBack(t));
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, EaseOutBack(t));

            if (t >= 1f)
            {
                Destroy(this); // Fin de l'animation
            }
        }
    }

    private void Appear()
    {
        // Faire apparaître le gem en grand et haut
        gameObject.SetActive(true);
        hasAppeared = true;
        isAnimating = true;
        elapsedTime = 0f;

        // Mémoriser les positions
        targetPosition = transform.localPosition; // Position finale (sur la couronne)
        startPosition = targetPosition + startPositionOffset; // Commence 0.5m plus haut

        transform.localPosition = startPosition;
        transform.localScale = startScale;

        // ✨ Ajouter le halo magique
        AddHaloEffect();
    }

    private void AddHaloEffect()
    {
        Light halo = gameObject.AddComponent<Light>();
        halo.type = LightType.Point;
        halo.range = 2f;
        halo.intensity = 8f;
        halo.color = Color.cyan;
    }

    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }
}
