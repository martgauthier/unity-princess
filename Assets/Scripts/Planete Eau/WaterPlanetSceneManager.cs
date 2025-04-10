using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class WaterPlanetSceneManager : MonoBehaviour
{
    public PlayableDirector leavePlanetTimeline;

    public void LeavePlanet()
    {
        Debug.Log("SceneManager wants to leave planet !");
        leavePlanetTimeline.Play();
        LaunchMenuPlanetAfterDelay();
    }

    async void LaunchMenuPlanetAfterDelay()
    {
        await Task.Delay(1860);//the duration of the animation
        if (CrownManager.Instance != null)
        {
            CrownManager.Instance.VisitPlanet("Water"); // Attention majuscule
        }
        else
        {
            Debug.LogWarning("CrownManager.Instance est null !");
        }
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
