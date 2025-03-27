using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityActivation : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 4f;

    public MeshRenderer lampRenderer;

    public Color glassTargetColor;
    public Color metalTargetColor;

    public Animator[] animatorsToActivate;

    private bool hasActivated = false;

    void Update()
    {
        if (hasActivated) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= activationDistance)
        {
            Activate();
            hasActivated = true;
        }
    }

    void Activate()
    {
        StartCoroutine(TransitionColors());

        foreach (Animator anim in animatorsToActivate)
        {
            if (anim != null)
            {
                anim.Play(0);
            }
        }
    }

    IEnumerator TransitionColors()
    {
        float duration = 3f;
        float elapsed = 0f;

        // Créer des instances de matériaux pour éviter de modifier les assets globaux
        Material[] materials = lampRenderer.materials;
        materials[0] = new Material(materials[0]);
        materials[1] = new Material(materials[1]);
        lampRenderer.materials = materials;

        Color glassStartColor = materials[0].GetColor("_Color");
        Color metalStartColor = materials[1].GetColor("_Color");

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            if (materials[0].HasProperty("_Color"))
                materials[0].SetColor("_Color", Color.Lerp(glassStartColor, glassTargetColor, t));

            if (materials[1].HasProperty("_Color"))
                materials[1].SetColor("_Color", Color.Lerp(metalStartColor, metalTargetColor, t));

            yield return null;
        }
    }
}