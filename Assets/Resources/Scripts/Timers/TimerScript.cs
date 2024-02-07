using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to update the timer in the HUD 
// and the Level Complete panel in the first level
public class TimerScript : MonoBehaviour 
{
    private Text timerText;
    private Text highScore;

    public Text LCtimerText;
    public Text LChighscore;

    private float timeSinceMove = 0f;
    private float milliseconds;
    private float seconds;
    private float minutes;


    void Start()
    {
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        highScore = GameObject.Find("HighScoreText").GetComponent<Text>();
        if (GlobalControl.Instance.lowestTime1 < 10000000000000){
            highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime1 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 * 1000f) % 1000;
        }
    }

    void Update()
    {
        if (!GlobalControl.Instance.pause)
        {
            if (GlobalControl.Instance.hasMoved)
            {
                if (GlobalControl.Instance.lowestTime1 < 10000000000000){
                highScore.text =
                    "Best time: " +
                    (int)(GlobalControl.Instance.lowestTime1 / 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 % 60f) +
                    ":" +
                    (int)(GlobalControl.Instance.lowestTime1 * 1000f) % 1000;
                }
                if (GlobalControl.Instance.finishedDelivery) //mailbox scene trigger
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
                    if (GlobalControl.Instance.lowestTime1 < 10000000000000){
                    highScore.text =
                        "Best time: " +
                        ((int)(GlobalControl.Instance.lowestTime1 / 60f)).ToString("00") +
                        ":" +
                        ((int)(GlobalControl.Instance.lowestTime1 % 60f)).ToString("00") +
                        ":" +
                        ((int)(GlobalControl.Instance.lowestTime1 * 1000f) % 1000).ToString("000");
                    }
                    if (timeSinceMove <= GlobalControl.Instance.lowestTime1)
                    {
                        GlobalControl.Instance.lowestTime1 = timeSinceMove;
                    }
                    LCtimerText.text = "Your time: " + timerText.text;
                    LChighscore.text = highScore.text;
                }
            }
        }
    }
}
