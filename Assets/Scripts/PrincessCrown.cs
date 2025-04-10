using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessCrown : MonoBehaviour
{
    public Transform crownAttachPoint; // Où apparaîtront les joyaux
    public GameObject waterGemPrefab;
    public GameObject fireGemPrefab;
    public GameObject airGemPrefab;

    private void Start()
    {
        if (CrownManager.Instance == null) return;

        foreach (string planet in CrownManager.Instance.visitedPlanets)
        {
            GameObject gemToSpawn = null;
            Vector3 positionOffset = Vector3.zero; // 🔥 Position différente selon le joyau

            switch (planet)
            {
                case "Water":
                    gemToSpawn = waterGemPrefab;
                    positionOffset = new Vector3(0f, 0.1f, 0f); // Water au-dessus
                    break;
                case "Fire":
                    gemToSpawn = fireGemPrefab;
                    positionOffset = new Vector3(-0.1f, 0f, 0f); // Fire à gauche
                    break;
                case "Air":
                    gemToSpawn = airGemPrefab;
                    positionOffset = new Vector3(0.1f, 0f, 0f); // Air à droite
                    break;
            }

            if (gemToSpawn != null && crownAttachPoint != null)
            {
                GameObject gem = Instantiate(gemToSpawn, crownAttachPoint);
                gem.transform.localPosition = positionOffset; 
                gem.AddComponent<GemPop>();

            }
        }
    }

}

