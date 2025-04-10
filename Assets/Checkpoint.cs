using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events; // needed to use UnityEvent
public class Checkpoint : MonoBehaviour
{
    public UnityEvent<CarIdentity, Checkpoint> onCheckpointEnter;
    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.GetComponent<CarIdentity>() != null)
        {
            // fire an event giving the entering gameObject and this checkpoint
            onCheckpointEnter.Invoke(collider.GetComponent<CarIdentity>(), this);
        }
    }
}
