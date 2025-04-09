using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueTextDescriptionManager : MonoBehaviour
{
    private bool shouldListenToSpacebar;

    public GameObject spaceBarImg;

    void Start()
    {
        
    }

    public void StopListeningToSpacebar()
    {
        shouldListenToSpacebar = false;
        spaceBarImg.SetActive(false);
    }

    public void StartListeningToSpacebar() {
        shouldListenToSpacebar = true;
        spaceBarImg.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldListenToSpacebar) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("He wants to start an animation !");
        }
    }

    void StartAppropriateTextCinematic()
    {
        //todo
    }
}
