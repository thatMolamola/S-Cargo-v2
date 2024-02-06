using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to trigger the end of the level upon 
// a collision between the player and the mailbox if all mail has been collected.
public class MailboxTrigger : MonoBehaviour
{
    private CutsceneControl cutsceneScript;
    private GlobalControl globalController;

    void Start()
    {
        cutsceneScript =
            GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
            globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (globalController.allMailCollected)
        {
            if (other.CompareTag("Player"))
            {
                cutsceneScript.mailboxTrigger = true;
            }
        }
    }
}
