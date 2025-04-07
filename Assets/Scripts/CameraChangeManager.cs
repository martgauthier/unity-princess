using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Playables;

public class CameraChangeManager : MonoBehaviour
{
    public GameObject followOutsideVirtualCamera;
    public GameObject lookAtInsideVCamera;
    public GameObject flowerCamera;

    void Start()
    {
    }

    public void OnPalaceInsideEnter()
    {
        followOutsideVirtualCamera.SetActive(false);
        lookAtInsideVCamera.SetActive(true);
    }

    public void OnFlowerColliderEnter()
    {
        lookAtInsideVCamera.SetActive(false);
        flowerCamera.SetActive(true);
    }
}
