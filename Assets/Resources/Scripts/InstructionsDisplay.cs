using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to show the instructions on the first level when the 
//instructionsStep integer is at the right value.
public class InstructionsDisplay : MonoBehaviour
{
    public GameObject text0;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    private GlobalControl globalController;
    private float timeLeft0;
    private float timeLeft1;
    private float timeLeft2;
    private float timeLeft3;

    private float firstTimer;

    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
            timeLeft0 = 10f;
            timeLeft1 = 10f; 
            timeLeft2 = 10f;
            timeLeft3 = 5f;
            firstTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (globalController.instructionsStep == 0) {
            text0.SetActive (false);
            text1.SetActive (false);
            text2.SetActive (false);
            text3.SetActive (false);
        }
        if (globalController.canMove) {
            if (globalController.instructionsStep == 4) {
                if (firstTimer > 3) 
                {
                    text0.SetActive(true);
                    text1.SetActive (false);
                    text2.SetActive (false);
                    text3.SetActive (false);
                    timeLeft0 -= Time.deltaTime; 
                    if (timeLeft0 < 0) {
                        globalController.instructionsStep = 0;
                    }
                } else {
                    firstTimer += Time.deltaTime;
                }
            }
        }
        if (globalController.instructionsStep == 1) {
            text0.SetActive (false);
            text1.SetActive (true);
            text2.SetActive (false);
            text3.SetActive (false);
            timeLeft1 -= Time.deltaTime; 
            if (timeLeft1 < 0) {
                globalController.instructionsStep = 0;
            }

        }
        if (globalController.instructionsStep == 2) {
            text0.SetActive (false);
            text1.SetActive (false);
            text2.SetActive (true);
            text3.SetActive (false);
            timeLeft2 -= Time.deltaTime; 
            if (timeLeft2 < 0) {
                globalController.instructionsStep = 0;
            }
        }
        if (globalController.instructionsStep == 3) {
            text0.SetActive (false);
            text1.SetActive (false);
            text2.SetActive (false);
            text3.SetActive (true);
            timeLeft3 -= Time.deltaTime; 
            if (timeLeft3 < 0) {
                globalController.instructionsStep = 0;
            }
        }
    }
}
