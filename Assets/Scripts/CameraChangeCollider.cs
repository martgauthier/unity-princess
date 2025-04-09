using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraChangeCollider : MonoBehaviour
{
    public GameObject cameraToEnable;
    public UnityEvent<GameObject> onColliderEnter;
    public UnityEvent onColliderExit;


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entered a collider !");
        // if entering object is tagged as the Player
        if (collider.CompareTag("Player"))
        {
            // fire an event giving the entering gameObject and this checkpoint
            cameraToEnable.SetActive(true);
            if(onColliderEnter != null) onColliderEnter.Invoke(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cameraToEnable.SetActive(false);
            onColliderExit.Invoke();
        }
    }
}
