using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraChangeCollider : MonoBehaviour
{
    public UnityEvent<GameObject, CameraChangeCollider> onColliderEnter;
    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.gameObject.tag == "Player")
        {
            // fire an event giving the entering gameObject and this checkpoint
            onColliderEnter.Invoke(collider.gameObject, this);
        }
    }
}
