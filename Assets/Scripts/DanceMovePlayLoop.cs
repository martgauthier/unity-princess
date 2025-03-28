using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DanceMovePlayLoop : MonoBehaviour
{
    public Animator statueAnimator;
    public string idleAnimationTrigger;


    public List<string> animationTriggers; // List of animation trigger names

    private int currentStatueAnimationIndex;

    public Animator playerAnimator;

    public KeyCode idleAnimationGuessKeyCode;//touche qui permet de tenter l'animation "aucune animation" (idle)

    //touches qui permettent de tenter les autres vraies animations. L'ordre des touches doit être le même que l'ordre des triggers fourni
    public List<KeyCode> animationsIndexPerKeycode = new List<KeyCode> { KeyCode.Z, KeyCode.E, KeyCode.R}; //should not contain the same keycode as "idleAnimationGuessKeyCode"

    public Renderer coloredStatusPlane;

    private bool playerCanPlay;

    private bool playerHasAlreadyTried;

    public AudioSource audioLoop;

    public int bpm;

    private float beatDurationInSeconds; //durée de 1 temps, par rapport au BPM

    private float timeBetweenMoves;

    public uint timeDurationExponent;

    void Start()
    {
        beatDurationInSeconds = 60.0f / bpm;//durée d'un temps
        timeBetweenMoves = beatDurationInSeconds * (float)Math.Pow(2, timeDurationExponent);

        audioLoop.Play();

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            Debug.Log("First");
            //Give a little pause
            SetGameSituationToIdle();
            yield return new WaitForSeconds(timeBetweenMoves);

            Debug.Log("Second");
            //show the move, player just has to watch and prepare to do it himself
            string triggeredStatueMove =TriggerRandomStatueMove();
            yield return new WaitForSeconds(timeBetweenMoves);

            Debug.Log("Third");
            //Give a little pause
            SetGameSituationToIdle();
            yield return new WaitForSeconds(timeBetweenMoves);

            Debug.Log("Fourth");
            //HERE ! The player has to reproduce the move
            TriggerStatueMove(triggeredStatueMove);
            playerCanPlay = true;
            yield return new WaitForSeconds(timeBetweenMoves);
        }
    }

    string TriggerRandomStatueMove()
    {
        string randomTrigger = animationTriggers[UnityEngine.Random.Range(0, animationTriggers.Count)];

        TriggerStatueMove(randomTrigger);

        return randomTrigger;
    }

    void TriggerStatueMove(string trigger)
    {
        currentStatueAnimationIndex = animationTriggers.IndexOf(trigger);
        statueAnimator.SetTrigger(trigger);
        ResetAnimationsAfterTimeout(timeBetweenMoves);
    }

    /**
     * A appeler pour mettre le jeu en situation de repos, entre les moves
     */
    void SetGameSituationToIdle()
    {
        coloredStatusPlane.material.color = Color.white;
        currentStatueAnimationIndex = -1;//equals to the IndexOf A in "indexesPerKeycode"
        playerCanPlay = false;
        playerHasAlreadyTried = false;
    }

    float GetAnimatorCurrentAnimationLength(Animator animator)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length > 0 ? stateInfo.length : 1f; // Default to 1s if unknown
    }

    void Update()
    {
        if (!playerCanPlay) return;

        KeyCode lastPressed = KeyCode.None;

        //Récupère la touche actuellement pressée
        if (Input.GetKeyDown(idleAnimationGuessKeyCode)) lastPressed = idleAnimationGuessKeyCode;
        foreach (KeyCode possibleKeyCode in animationsIndexPerKeycode)
        {
            if(Input.GetKeyDown(possibleKeyCode)) lastPressed = possibleKeyCode;
        }

        if (lastPressed != KeyCode.None && !playerHasAlreadyTried)
        {
            AnimatePlayer(lastPressed);
            UpdateMoveValidityStatus(lastPressed);
            playerHasAlreadyTried = true;
        }
    }

    void AnimatePlayer(KeyCode keyPressed)
    {
        int animationToStartIndex = animationsIndexPerKeycode.IndexOf(keyPressed);

        if (0 <= animationToStartIndex && animationToStartIndex <= animationTriggers.Count) playerAnimator.SetTrigger(animationTriggers[animationToStartIndex]);
        else playerAnimator.SetTrigger(idleAnimationTrigger);
    }

    void UpdateMoveValidityStatus(KeyCode lastKeyPressed)
    {
        int animationToStartIndex = animationsIndexPerKeycode.IndexOf(lastKeyPressed);

        if (animationToStartIndex == currentStatueAnimationIndex)
        {
            coloredStatusPlane.material.color = Color.green;
        }
        else
        {
            coloredStatusPlane.material.color = Color.red;
        }
    }

    private async void ResetAnimationsAfterTimeout(float lengthInSeconds)
    {
        await Task.Delay(System.Convert.ToInt32(lengthInSeconds * 1000));
        playerAnimator.SetTrigger(idleAnimationTrigger);
        statueAnimator.SetTrigger(idleAnimationTrigger);
    }
}
