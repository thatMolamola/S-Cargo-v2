using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to trigger the end of the level upon 
// a collision between the player and the mailbox if all mail has been collected.
public class MailboxTrigger : MonoBehaviour
{
    [SerializeField] private CutsceneControl cutsceneScript;
    [SerializeField] private TimerScript timer;
    [SerializeField] private GameObject iDisplay;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GlobalControl.Instance.allMailCollected)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(cutsceneScript.MailboxCutscene());
                iDisplay.SetActive(false);
            }
        }
    }
}
