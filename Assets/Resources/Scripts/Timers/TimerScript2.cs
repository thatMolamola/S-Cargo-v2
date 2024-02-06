using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to update the timer in the HUD 
// and the Level Complete panel in the second level
public class TimerScript2 : MonoBehaviour
{
    private Text timerText;

    private Text highScore;

    public Text LCtimerText;

    public Text LChighscore;

    private float timeSinceMove = 0f;

    private float milliseconds;

    private float seconds;

    private float minutes;

    private GlobalControl globalController;

    private CutsceneControl cutsceneScript;

    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        highScore = GameObject.Find("HighScoreText").GetComponent<Text>();
        cutsceneScript =
            GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
        if (globalController.lowestTime2 < 10000000000000){
        highScore.text =
                    "Best time: " +
                    (int)(globalController.lowestTime2 / 60f) +
                    ":" +
                    (int)(globalController.lowestTime2 % 60f) +
                    ":" +
                    (int)(globalController.lowestTime2 * 1000f) % 1000;
        }
    }

    void Update()
    {
        if (!globalController.pause)
        {
            if (globalController.hasMoved)
            {
                 if (globalController.lowestTime2 < 10000000000000){
                highScore.text =
                    "Best time: " +
                    (int)(globalController.lowestTime2 / 60f) +
                    ":" +
                    (int)(globalController.lowestTime2 % 60f) +
                    ":" +
                    (int)(globalController.lowestTime2 * 1000f) % 1000;
                 }
                if (!cutsceneScript.mailboxTrigger)
                {
                    timeSinceMove += Time.deltaTime;
                    minutes = (int)(timeSinceMove / 60f);
                    seconds = (int)(timeSinceMove % 60f);
                    milliseconds = (int)(timeSinceMove * 1000f) % 1000;
                    timerText.text =
                        minutes.ToString("00") +
                        ":" +
                        seconds.ToString("00") +
                        ":" +
                        milliseconds.ToString("000");
                }
                else
                {
                     if (globalController.lowestTime2 < 10000000000000){
                    highScore.text =
                        "Best time: " +
                        ((int)(globalController.lowestTime2 / 60f)).ToString("00") +
                        ":" +
                        ((int)(globalController.lowestTime2 % 60f)).ToString("00") +
                        ":" +
                        ((int)(globalController.lowestTime2 * 1000f) % 1000).ToString("000");
                     }
                    if (timeSinceMove <= globalController.lowestTime2)
                    {
                        globalController.lowestTime2 = timeSinceMove;
                    }
                    LCtimerText.text = "Your time: " + timerText.text;
                    LChighscore.text = highScore.text;
                }
            }
        }
    }
}

