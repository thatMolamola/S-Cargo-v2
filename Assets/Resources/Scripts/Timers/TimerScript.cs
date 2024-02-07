using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to update the timer in the HUD 
// and the Level Complete panel in the first level
public class TimerScript : MonoBehaviour 
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text highScore;

    [SerializeField] private Text LCtimerText;
    [SerializeField] private Text LChighscore;
    [SerializeField] private int levelNum;
    private int minutes, seconds, milliseconds;
    private float startTime;
    [SerializeField] private CutsceneControl cutsceneScript;
    private float timeSinceMove = 0f;

    void Start()
    {
        if (levelNum == 1) {
            if (GlobalControl.Instance.lowestTime1 < 10000000000000){
            highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime1 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 * 1000f) % 1000;
            }
        } else if (levelNum == 2) {
            if (GlobalControl.Instance.lowestTime2 < 10000000000000){
            highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime2 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime2 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime2 * 1000f) % 1000;
            }
        } else if (levelNum == 3) {
            if (GlobalControl.Instance.lowestTime3 < 10000000000000){
            highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime3 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime3 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime3 * 1000f) % 1000;
            }
        }
        
    }

    void Update()
    {
        if (!GlobalControl.Instance.pause)
        {
            if (GlobalControl.Instance.hasMoved)
            {
                if (levelNum == 1) {
                    if (GlobalControl.Instance.lowestTime1 < 10000000000000){
                        highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime1 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 * 1000f) % 1000;
                    }
                } else if (levelNum == 2) {
                    if (GlobalControl.Instance.lowestTime2 < 10000000000000){
                        highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime2 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime2 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime2 * 1000f) % 1000;
                    }
                } else if (levelNum == 3) {
                    if (GlobalControl.Instance.lowestTime3 < 10000000000000){
                        highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime3 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime3 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime3 * 1000f) % 1000;
                    }
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
                    if (levelNum == 1) {
                        if (GlobalControl.Instance.lowestTime1 < 10000000000000){
                        highScore.text =
                                "Best time: " +
                                (int)(GlobalControl.Instance.lowestTime1 / 60f) +
                                ":" +
                                (int)(GlobalControl.Instance.lowestTime1 % 60f) +
                                ":" +
                                (int)(GlobalControl.Instance.lowestTime1 * 1000f) % 1000;
                        }
                    } else if (levelNum == 2) {
                        if (GlobalControl.Instance.lowestTime2 < 10000000000000){
                        highScore.text =
                                "Best time: " +
                                (int)(GlobalControl.Instance.lowestTime2 / 60f) +
                                ":" +
                                (int)(GlobalControl.Instance.lowestTime2 % 60f) +
                                ":" +
                                (int)(GlobalControl.Instance.lowestTime2 * 1000f) % 1000;
                        }
                    } else if (levelNum == 3) {
                        if (GlobalControl.Instance.lowestTime3 < 10000000000000){
                        highScore.text =
                                "Best time: " +
                                (int)(GlobalControl.Instance.lowestTime3 / 60f) +
                                ":" +
                                (int)(GlobalControl.Instance.lowestTime3 % 60f) +
                                ":" +
                                (int)(GlobalControl.Instance.lowestTime3 * 1000f) % 1000;
                        }
                    }

                    if (levelNum == 1) {
                        if (timeSinceMove <= GlobalControl.Instance.lowestTime1)
                        {
                            GlobalControl.Instance.lowestTime1 = timeSinceMove;
                        } 
                    } else if (levelNum == 2) {
                        if (timeSinceMove <= GlobalControl.Instance.lowestTime2)
                        {
                            GlobalControl.Instance.lowestTime2 = timeSinceMove;
                        } 
                    } else if (levelNum == 3) {
                        if (timeSinceMove <= GlobalControl.Instance.lowestTime3)
                        {
                            GlobalControl.Instance.lowestTime3 = timeSinceMove;
                        } 
                    }
                    
                    LCtimerText.text = "Your time: " + timerText.text;
                    LChighscore.text = highScore.text;
                }
            }
        }
    }
}
