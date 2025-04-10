using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketPlatformCollisionner : MonoBehaviour
{
    public UnityEvent onPlatformEnterAndAllDone;
    public GameObject textToDisplayWhenNotDone;

    public CheckEverythingIsDoneManager checkEverythingIsDoneManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Reached the leaving platform !");
            if (checkEverythingIsDoneManager.checkAllStatuesAreVisited())
            {
                onPlatformEnterAndAllDone.Invoke();
            }
            else
            {
                textToDisplayWhenNotDone.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!checkEverythingIsDoneManager.checkAllStatuesAreVisited())
            {
                textToDisplayWhenNotDone.SetActive(false);
            }
        }
    }
}