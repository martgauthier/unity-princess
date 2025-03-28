using System.Collections;
using System.Collections.Generic;
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
        LaunchLeavingPlanetAnimation();
    }

    public void LoadWindPlanet()
    {
        LaunchLeavingPlanetAnimation();
    }
}
