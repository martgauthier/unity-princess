using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public PlayableDirector timelineDirector;

    void LaunchLeavingPlanetAnimation()
    {
        timelineDirector.Play();
    }

    public void LoadFirePlanet()
    {
        LaunchLeavingPlanetAnimation();
    }

    public void LoadWaterPlanet()
    {
        Debug.Log("Water scene button clicked !");
        LaunchLeavingPlanetAnimation();

        Debug.Log("Launched the async task of water planet");
        LaunchWaterPlanetAfterTimeout();
    }

    public void LoadWindPlanet()
    {
        LaunchLeavingPlanetAnimation();
    }

    async void LaunchWaterPlanetAfterTimeout()
    {
        await Task.Delay(1750);//duration of the timeline in milliseconds
        Debug.Log("Water scene started !");
        SceneManager.LoadScene(1, LoadSceneMode.Single);//id of the water scene
    }
}
