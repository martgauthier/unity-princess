using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketPlatformCollisionner : MonoBehaviour
{
    public UnityEvent onPlatformEnter;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Reached the leaving platform !");
            onPlatformEnter.Invoke();
        }
    }
}
