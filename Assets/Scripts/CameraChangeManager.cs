using UnityEngine.Events;
using UnityEngine;

public class CameraChangeManager : MonoBehaviour
{
    public GameObject followVirtualCamera;
    public GameObject fixedPointVirtualCamera;

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
}
