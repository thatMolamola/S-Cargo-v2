using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to act as a temporary trigger for instructions on the first level
// the script updates the globalController.instructionStep, and then de-activates the gameObject
public class Instructions : MonoBehaviour
{
    [SerializeField] private int triggerNumber; 
    [SerializeField] private InstructionsDisplay iD; 
    private bool coroutineStarted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.CompareTag("Instructions") && !coroutineStarted)
            {
                coroutineStarted = true;
                StartCoroutine(iD.ShowTriggerInstructions(triggerNumber));
            }
        }
    }
}
