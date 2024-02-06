using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to update the times on the LevelSelect scene 
// with the corresponding values stored in GlobalController.
public class LevelSelectTimes : MonoBehaviour
{
    [SerializeField] private Text timerText1, timerText2, timerText3;

    void Start()
    {
        float time1 = GlobalControl.Instance.lowestTime1;
        float time2 = GlobalControl.Instance.lowestTime2;
        float time3 = GlobalControl.Instance.lowestTime3;

        if (time1 < 10000000000000){
            timerText1.text =
                    "Best time: " +
                    (int)(time1 / 60f) +
                    ":" +
                    (int)(time1 % 60f) +
                    ":" +
                    (int)(time1 * 1000f) % 1000;
        }
        if (time2 < 10000000000000){
            timerText2.text =
                    "Best time: " +
                    (int)(time2 / 60f) +
                    ":" +
                    (int)(time2 % 60f) +
                    ":" +
                    (int)(time2 * 1000f) % 1000;
        }
        if (time3 < 10000000000000){
            timerText3.text =
                    "Best time: " +
                    (int)(time3 / 60f) +
                    ":" +
                    (int)(time3 % 60f) +
                    ":" +
                    (int)(time3 * 1000f) % 1000;
        }
    }
}
