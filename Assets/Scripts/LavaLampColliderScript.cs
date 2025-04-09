using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class LavaLampColliderScript : MonoBehaviour
{
    public PlayableDirector entryTimelineToPlay;
    public GameObject vCameraToDisable;


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entered a collider !");
        // if entering object is tagged as the Player
        if (collider.CompareTag("Player"))
        {
            // fire an event giving the entering gameObject and this checkpoint
            entryTimelineToPlay.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entryTimelineToPlay.Stop();
            vCameraToDisable.SetActive(false);
        }
    }
}
