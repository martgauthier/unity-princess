using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayersControl playerControls;
    public CarMovement firstAiControls;
    public CarMovement secondAiControls;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableGame()
    {
        Debug.Log("Disabled all game elements");
        if (firstAiControls != null)
        {
            firstAiControls.enabled = false;
        }
        if (secondAiControls != null)
        {
            secondAiControls.enabled = false;
        }
        if (playerControls != null)
        {
            playerControls.enabled = false;
        }
    }

    public void EnableGame()
    {
        playerControls.enabled = true;
        firstAiControls.enabled = true;
        secondAiControls.enabled = true;
    }
}
