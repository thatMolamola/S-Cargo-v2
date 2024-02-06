using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to trigger the end of the level upon 
// a collision between the player and the mailbox if all mail has been collected.
public class MailboxTrigger : MonoBehaviour
{
    private CutsceneControl cutsceneScript;

    void Start()
    {
        cutsceneScript = GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GlobalControl.Instance.allMailCollected)
        {
            if (other.CompareTag("Player"))
            {
                cutsceneScript.mailboxTrigger = true;
            }
        }
    }
}
