using UnityEngine;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrownManager : MonoBehaviour
{
    private static CrownManager _instance;
    public static CrownManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CrownManager>();
                if (_instance == null)
                {
                    GameObject crownManagerObj = new GameObject("CrownManager");
                    _instance = crownManagerObj.AddComponent<CrownManager>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public List<string> visitedPlanets = new List<string>();

	private void Start()
	{
/*
    	if (visitedPlanets.Count < 3)
    	{
        	visitedPlanets.Add("Water");
        	visitedPlanets.Add("Fire");
        	visitedPlanets.Add("Air");
    	}

*/
		StartCoroutine(CheckVictoryCondition());

	}

    [Header("End Game Settings")]
	public Canvas gameCanvas; 
    public Animator princessAnimator;       
    public Transform crownAttachPoint;        
    private bool hasCelebrated = false;   
	public Canvas victoryCanvas; 
    

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
{
    if (!hasCelebrated && visitedPlanets.Count >= 3)
    {
        hasCelebrated = true;
        StartCoroutine(PlayVictorySequence());
    }
}


    public void VisitPlanet(string planetName)
    {
        if (!visitedPlanets.Contains(planetName))
        {
            visitedPlanets.Add(planetName);
        }
    }
    private void RefreshReferences()
    {
       
        GameObject princess = GameObject.Find("princess-last-dancing"); // Remplace "Princess" par le vrai nom de ton GameObject princesse si besoin

        if (princess != null)
        {
            // Animator
            if (princessAnimator == null)
            {
                princessAnimator = princess.GetComponent<Animator>();
            }

            // CrownAttachPoint enfant
            if (crownAttachPoint == null)
            {
                Transform crown = princess.transform.Find("CrownAttachmentPoint");
                if (crown != null)
                {
                    crownAttachPoint = crown;
                }
            }
        }
        else
        {
            Debug.LogError("Princess non trouvée dans la scène !");
        }

        
        if (gameCanvas == null)
        {
            GameObject canvasObj = GameObject.Find("Canvas");
            if (canvasObj != null)
            {
                gameCanvas = canvasObj.GetComponent<Canvas>();
            }
        }

        
        if (victoryCanvas == null)
        {
            GameObject victoryObj = GameObject.Find("VictoryCanvas");
            if (victoryObj != null)
            {
                victoryCanvas = victoryObj.GetComponent<Canvas>();
            }
        }

    }



    private IEnumerator AnimateGem(Transform gem)
{
    Vector3 startPos = gem.position;
    Vector3 centerPoint = startPos + new Vector3(0, 100f, 0); // 🚀 Monter 6 unités plus haut (3x plus haut)
    Vector3 startScale = gem.localScale;

    float riseDuration = 2f; // Phase montée
    float spinDuration = 10f; // Phase tourbillon
    float totalDuration = riseDuration + spinDuration;

    float elapsed = 0f;

    // Phase 1 : montée verticale rapide
    while (elapsed < riseDuration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / riseDuration);

        gem.position = Vector3.Lerp(startPos, centerPoint, t); // Monter tout droit
        gem.localScale = Vector3.Lerp(startScale, startScale * 1.5f, t); // Grandir doucement

        yield return null;
    }

    // Reset temps pour phase 2
    elapsed = 0f;

    // Phase 2 : tourbillon plus large autour du centre
    while (elapsed < spinDuration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / spinDuration);

        float angle = t * 1050f; // Deux tours complets (720°)
        float radius = Mathf.Lerp(0.5f, 100f, t); // 🌟 Rayon plus large : commence à 0.5 et finit à 2 unités !

        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
        gem.position = centerPoint + offset + new Vector3(0, Mathf.Sin(t * Mathf.PI) * 0.5f, 0); // Effet petit haut/bas
        gem.localScale = Vector3.Lerp(startScale * 1.5f, startScale * 4f, t); // Continuer de grandir

        yield return null;
    }

    // Explosion finale
    Destroy(gem.gameObject);
	}

private IEnumerator AnimateGem(Transform gem, int gemIndex, int totalGems)
{
    Vector3 centerPoint = crownAttachPoint.position + new Vector3(0, 5f, 0); // 🎯 Centre de la ronde
    Vector3 startScale = gem.localScale;

    float spinDuration = 4f;
    float elapsed = 0f;

    // Définir l'angle de départ de chaque gem
    float angleOffset = (360f / totalGems) * gemIndex; 

    while (elapsed < spinDuration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / spinDuration);

        float globalAngle = (t * 360f) + angleOffset; // Chaque gem tourne autour du cercle

        float radius = 80f; // 🌟 Rayon constant
        Vector3 offset = new Vector3(Mathf.Cos(globalAngle * Mathf.Deg2Rad), 0, Mathf.Sin(globalAngle * Mathf.Deg2Rad)) * radius;

        gem.position = centerPoint + offset;
        gem.localScale = Vector3.Lerp(startScale, startScale * 10f, t); // Grandir un peu pendant la ronde

        yield return null;
    }
	// Explosion visuelle rapide
float explosionDuration = 0.3f;
float explosionElapsed = 0f;

Material gemMaterial = gem.GetComponent<Renderer>()?.material; // récupérer le material du gem
Color startColor = gemMaterial != null ? gemMaterial.color : Color.white;

Vector3 currentScale = gem.localScale;

while (explosionElapsed < explosionDuration)
{
    explosionElapsed += Time.deltaTime;
    float t = Mathf.Clamp01(explosionElapsed / explosionDuration);

    // Grossir rapidement
    gem.localScale = Vector3.Lerp(currentScale, currentScale * 3f, t);

    // Devenir transparent
    if (gemMaterial != null)
    {
        Color newColor = startColor;
        newColor.a = Mathf.Lerp(1f, 0f, t);
        gemMaterial.color = newColor;
    }

    yield return null;
}

    // Explosion finale
    Destroy(gem.gameObject);
}



private IEnumerator PlayVictorySequence()
{
    RefreshReferences();
    yield return new WaitForSeconds(0.1f);
    if (gameCanvas != null)
    {
        foreach (Transform child in gameCanvas.transform)
        {
            child.gameObject.SetActive(false); // Désactive tous les textes/images
        }
    }

    yield return new WaitForSeconds(3f); // Attendre avant de commencer

    // Faire monter le CrownAttachPoint
    StartCoroutine(MoveCrownAttachPointUp());

    // 1. Jouer la danse de la princesse
    if (princessAnimator != null)
    {
        princessAnimator.SetTrigger("Celebrate");
    }

    yield return new WaitForSeconds(1f); // Petite pause avant de lancer les gems

    // 2. Faire exploser les gems
    if (crownAttachPoint != null)
    {
        int i = 0;
        foreach (Transform gem in crownAttachPoint)
        {
            StartCoroutine(AnimateGem(gem, i, crownAttachPoint.childCount));
            i++;
        }
    }

    if (victoryCanvas != null)
    {
        foreach (Transform child in victoryCanvas.transform)
        {
            child.gameObject.SetActive(true); // active tous les textes/images
        }
    }
}




private IEnumerator CheckVictoryCondition()
{
    while (visitedPlanets.Count < 3)
    {
        yield return null;
    }

    if (!hasCelebrated)
    {
        hasCelebrated = true;
        StartCoroutine(PlayVictorySequence());
    }
}


	private IEnumerator MoveCrownAttachPointUp()
{
    Vector3 startPos = crownAttachPoint.position;
    Vector3 targetPos = startPos + new Vector3(0, 100f, 0); // 🎯 Monter 5 mètres au-dessus

    float duration = 2f; // Combien de secondes pour monter
    float elapsed = 0f;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        crownAttachPoint.position = Vector3.Lerp(startPos, targetPos, t);

        yield return null;
    }
}

}
