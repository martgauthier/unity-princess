using UnityEngine;
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
                // Chercher s'il existe déjà un CrownManager dans la scène
                _instance = FindObjectOfType<CrownManager>();

                // Sinon, créer un nouveau GameObject CrownManager
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

    public void VisitPlanet(string planetName)
    {
        if (!visitedPlanets.Contains(planetName))
        {
            visitedPlanets.Add(planetName);
        }
    }
}