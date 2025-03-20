using UnityEngine;

public class ColorTransition: MonoBehaviour
{
    public Transform player;  // Référence au personnage (cylindre)
    public Color startColor = Color.blue;
    public Color endColor = Color.red;
    public float duration = 2.0f;
    public float activationDistance = 4f; // Distance d'activation

    private Renderer cubeRenderer;
    private float elapsedTime = 0f;
    private bool hasTransitioned = false;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = startColor;
    }

    void Update()
    {
        if (player == null) return; // Vérifie si le joueur est assigné

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activationDistance && !hasTransitioned)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            cubeRenderer.material.color = Color.Lerp(startColor, endColor, t);

            if (elapsedTime >= duration)
            {
                hasTransitioned = true; // Marque la transition comme terminée
            }
        }
    }
}