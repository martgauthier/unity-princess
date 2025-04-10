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
        StartCoroutine(SpawnGemsDelayed());
    }

    private IEnumerator SpawnGemsDelayed()
    {
        yield return new WaitForSeconds(0.1f); // attendre un peu

        if (CrownManager.Instance == null) yield break;

        foreach (string planet in CrownManager.Instance.visitedPlanets)
        {
            GameObject gemToSpawn = null;
            Vector3 positionOffset = Vector3.zero;

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
                gem.transform.localRotation = Quaternion.identity;
                gem.transform.localScale = Vector3.one;
                gem.AddComponent<GemPop>();
            }
        }
    }
}

