using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Playables;

public class CameraChangeManager : MonoBehaviour
{
    public GameObject followVirtualCamera;
    public GameObject fixedPointVirtualCamera;
    public PlayableDirector timeline;

    void Start()
    {
    }

    public void OnFollowPlayerColliderEnter(GameObject go, CameraChangeCollider collider)
    {
        Debug.Log("Entered follow player collider !");
        followVirtualCamera.SetActive(true);
        fixedPointVirtualCamera.SetActive(false);
    }

    public void OnFixedPointCameraColliderEnter(GameObject go, CameraChangeCollider collider)
    {
        Debug.Log("Entered fixed point collider !");
        followVirtualCamera.SetActive(false);
        fixedPointVirtualCamera.SetActive(true);
    }

    public void OnTimelineStartColliderEnter(GameObject go, CameraChangeCollider collider)
    {
        Debug.Log("Timeline started !");
        timeline.Play();
        OnFollowPlayerColliderEnter(go, collider);//also turn back to the initial state
    }
}
