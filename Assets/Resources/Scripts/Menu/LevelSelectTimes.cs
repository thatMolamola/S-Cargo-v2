using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to update the times on the LevelSelect scene 
// with the corresponding values stored in GlobalController.
public class LevelSelectTimes : MonoBehaviour
{
    private GlobalControl globalController;
    private Text timerText1;
    private Text timerText2;
    private Text timerText3;

    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
        timerText1 = GameObject.Find("Speed1").GetComponent<Text>();
        timerText2 = GameObject.Find("Speed2").GetComponent<Text>();
        timerText3 = GameObject.Find("Speed3").GetComponent<Text>();
        if (globalController.lowestTime1 < 10000000000000){
            timerText1.text =
                    "Best time: " +
                    (int)(globalController.lowestTime1 / 60f) +
                    ":" +
                    (int)(globalController.lowestTime1 % 60f) +
                    ":" +
                    (int)(globalController.lowestTime1 * 1000f) % 1000;
        }
        if (globalController.lowestTime2 < 10000000000000){
            timerText2.text =
                    "Best time: " +
                    (int)(globalController.lowestTime2 / 60f) +
                    ":" +
                    (int)(globalController.lowestTime2 % 60f) +
                    ":" +
                    (int)(globalController.lowestTime2 * 1000f) % 1000;
        }
        if (globalController.lowestTime3 < 10000000000000){
            timerText3.text =
                    "Best time: " +
                    (int)(globalController.lowestTime3 / 60f) +
                    ":" +
                    (int)(globalController.lowestTime3 % 60f) +
                    ":" +
                    (int)(globalController.lowestTime3 * 1000f) % 1000;
        }
    }
}
