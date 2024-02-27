using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to show the instructions on the first level when the 
//instructionsStep integer is at the right value.

//TO-DO: CHANGE THIS ENTIRE SCRIPT
public class InstructionsDisplay : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject textbox;
    
    void Start()
    {
        StartCoroutine(ShowOpenInstructions());
    }

    IEnumerator ShowOpenInstructions() {
        if (GlobalControl.Instance.cutsceneEnabled) {
            yield return new WaitForSeconds(8f);
        } else {
            yield return new WaitForSeconds(3f);
        }
        textbox.SetActive(true);
        text.text = "Hey kid, use the arrow keys to move, and the x key to jump.";
        yield return new WaitForSeconds(5f);
        textbox.SetActive(false);
    }

    public IEnumerator ShowTriggerInstructions(int passedNum) { 
        textbox.SetActive(true);
        if (passedNum == 1) {
             text.text = "Hey kid. To stick to a surface, like a wall or ceiling, press the arrow key pointing into the surface. It's pretty easy.";
        } else if (passedNum == 2) {text.text = "Avoid the spikes, kid. Do I have to tell you everything myself? For more help, press the i key.";}
        else if (passedNum == 3) {text.text = "Hey kid. Make sure you collect ALL the mail before delivering it. Check the cave to your right.";}
        yield return new WaitForSeconds(5f);
        textbox.SetActive(false);
    }
}
