using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPlatformCollisionner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Reached the leaving platform !");
        }
    }
}
