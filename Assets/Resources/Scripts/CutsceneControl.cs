using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is designed to play the cutscenes at the beginning and end of the level
// if cutsceneEnabled is true. 
public class CutsceneControl : MonoBehaviour
{
    public GameObject mail;
    public GameObject ohNo;
    public GameObject blackLight;

    private float timeLeft = 6f;
    private bool timerIsRunning = true;
    public bool mailboxTrigger = false; 

    public GameObject speechBubble; 
    public GameObject closingText1; 
    public GameObject closingText2; 
    public GameObject closingText3;
    public GameObject SnailSprite1;
    public GameObject RecipientImage;
    public GameObject LevelComplete;


    private float closingTimer = 15f; 

    private GlobalControl globalController;
    void Start(){
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (globalController.cutsceneEnabled){
            if (timerIsRunning) {
                globalController.pause = true;
                globalController.canMove = false;
                timeLeft -= Time.deltaTime;
                if (timeLeft < .7) {
                    ohNo.SetActive (true);
                }
                if ( timeLeft < 0 )
                {
                    ohNo.SetActive(false);
                    mail.SetActive (false);
                    blackLight.SetActive (false);
                    timerIsRunning = false;
                    globalController.canMove = true;
                    globalController.pause = false;
                }
            } 
        } else {
            ohNo.SetActive(false);
            mail.SetActive (false);
            blackLight.SetActive (false);
        }
        if (mailboxTrigger) {
            globalController.canMove = false;
            if (globalController.cutsceneEnabled){
                closingTimer -= Time.deltaTime;
                if (closingTimer < 15) {
                    speechBubble.SetActive (true);
                    closingText1.SetActive (true);
                    SnailSprite1.SetActive(true);
                }

                if (closingTimer < 10 )
                {
                    closingText2.SetActive (true);
                    SnailSprite1.SetActive(false);
                    RecipientImage.SetActive(true);
                    closingText1.SetActive (false);
                }

                if (closingTimer < 5 )
                {
                    closingText3.SetActive (true);
                    closingText2.SetActive (false);
                }

                if (closingTimer < 0)
                {
                    closingText3.SetActive (false);
                    RecipientImage.SetActive(false);
                    speechBubble.SetActive (false);
                    LevelComplete.SetActive(true);
                }
            } else {
            LevelComplete.SetActive(true);  
        } 
        } 
    }
}
