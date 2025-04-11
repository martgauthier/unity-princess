using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayersControl : MonoBehaviour
{
    private float inputX;
    private float inputY;

    private Vector2 normalizedInput;

    public UnityEvent<Vector2> onInput;

    public Animator voileAnimator;
    public Animator fanAnimator;

    private bool shouldDrive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        normalizedInput=new Vector2(inputX, inputY).normalized;

        if(inputY > 0 && !shouldDrive)
        {
            shouldDrive = true;
            voileAnimator.SetTrigger("gonfle_on");
            fanAnimator.SetTrigger("ventilateur_on");
        }
        else if(shouldDrive && inputY == 0)
        {
            shouldDrive = false;
            voileAnimator.SetTrigger("gonfle_off");
            fanAnimator.SetTrigger("ventilateur_off");
        }

        onInput.Invoke(normalizedInput);
    }
}
