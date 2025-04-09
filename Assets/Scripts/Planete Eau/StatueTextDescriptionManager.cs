using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class StatueTextDescriptionManager : MonoBehaviour
{
    private bool shouldListenToSpacebar;

    public GameObject spaceBarImg;

    private GameObject listenedGameObject;

    public PlayableDirector flowerTimeline;
    public PlayableDirector lavalampTimeline;
    public PlayableDirector champignonTimeline;



    void Start()
    {
        
    }

    public void StopListeningToSpacebar()
    {
        shouldListenToSpacebar = false;
        spaceBarImg.SetActive(false);
    }

    public void StartListeningToSpacebar(GameObject gameObject) {
        shouldListenToSpacebar = true;
        spaceBarImg.SetActive(true);
        listenedGameObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldListenToSpacebar) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("He wants to start an animation for the collider : !");
            Debug.Log(listenedGameObject.tag);

            if(listenedGameObject.CompareTag("flower_collider"))
            {
                Debug.Log("launch specific flower animation");
                flowerTimeline.Play();
                shouldListenToSpacebar = false;//to prevent calling multiple times the timeline
                StartListeningToSpacebarAfterTimeout();
            }
            else if(listenedGameObject.CompareTag("lavalamp_collider"))
            {
                Debug.Log("launch specific lavalamp animation");
                lavalampTimeline.Play();
                shouldListenToSpacebar = false;//to prevent calling multiple times the timeline
                StartListeningToSpacebarAfterTimeout();
            }
            else if(listenedGameObject.CompareTag("hajar_collider"))
            {
                Debug.Log("launch specific champignon animation");
                champignonTimeline.Play();
                shouldListenToSpacebar = false;//to prevent calling multiple times the timeline
                StartListeningToSpacebarAfterTimeout();
            }
        }
    }

    async void StartListeningToSpacebarAfterTimeout()
    {
        await Task.Delay(5000);//duration of the timeline
        StartListeningToSpacebar(listenedGameObject);
    }
}
