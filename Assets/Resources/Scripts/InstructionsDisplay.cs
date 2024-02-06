using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to show the instructions on the first level when the 
//instructionsStep integer is at the right value.
public class InstructionsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject text0, text1, text2, text3;
    private float timeLeft1, timeLeft2, timeLeft3;

    void Start()
    {
            timeLeft1 = 10f; 
            timeLeft2 = 10f;
            timeLeft3 = 5f;
            StartCoroutine(ShowInstructions());
    }

    IEnumerator ShowInstructions() {
        if (GlobalControl.Instance.cutsceneEnabled) {
            yield return new WaitForSeconds(8f);
        } else {
            yield return new WaitForSeconds(3f);
        }
        text0.SetActive(true);
        yield return new WaitForSeconds(8f);
        text0.SetActive (false);
        text1.SetActive (true);
        yield return new WaitForSeconds(8f);
        text1.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalControl.Instance.instructionsStep == 1) {
            text0.SetActive (false);
            text1.SetActive (true);
            text2.SetActive (false);
            text3.SetActive (false);
            timeLeft1 -= Time.deltaTime; 
            if (timeLeft1 < 0) {
                GlobalControl.Instance.instructionsStep = 0;
            }

        }
        if (GlobalControl.Instance.instructionsStep == 2) {
            text0.SetActive (false);
            text1.SetActive (false);
            text2.SetActive (true);
            text3.SetActive (false);
            timeLeft2 -= Time.deltaTime; 
            if (timeLeft2 < 0) {
                GlobalControl.Instance.instructionsStep = 0;
            }
        }
        if (GlobalControl.Instance.instructionsStep == 3) {
            text0.SetActive (false);
            text1.SetActive (false);
            text2.SetActive (false);
            text3.SetActive (true);
            timeLeft3 -= Time.deltaTime; 
            if (timeLeft3 < 0) {
                GlobalControl.Instance.instructionsStep = 0;
            }
        }
    }
}
