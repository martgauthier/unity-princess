using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveAirPlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveTheAirPlanet()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
