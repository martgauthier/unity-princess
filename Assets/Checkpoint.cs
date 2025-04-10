using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; // needed to use UnityEvent
public class Checkpoint : MonoBehaviour
{
    public UnityEvent<GameObject, Checkpoint> onCheckpointEnter;
    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.gameObject.tag == "Player")
        {
            // fire an event giving the entering gameObject and this checkpoint
            onCheckpointEnter.Invoke(collider.gameObject, this);
        }
    }
}
