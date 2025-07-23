using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is designed to play the cutscenes at the beginning and end of the level
// if cutsceneEnabled is true. 
public class CutsceneControl : MonoBehaviour
{
    public GameObject mail;
    public GameObject ohNo;
    public GameObject blackLight;

    public bool mailboxTrigger = false;

    public GameObject speechBubble; 
    public GameObject closingText1; 
    public GameObject closingText2; 
    public GameObject closingText3;
    public GameObject SnailSprite1;
    public GameObject RecipientImage;
    public GameObject LevelComplete;

    void Start() {
        if (GlobalControl.Instance.cutsceneEnabled){
            StartCoroutine(OpeningCutsceneCall());
        } else {
            ohNo.SetActive(false);
            mail.SetActive (false);
            blackLight.SetActive (false);
        }
    }

    IEnumerator OpeningCutsceneCall() {
        GlobalControl.Instance.paused = true;
        GlobalControl.Instance.canMove = false;
        yield return new WaitForSeconds(5.3f);
        ohNo.SetActive(true);
        yield return new WaitForSeconds(.7f);
        ohNo.SetActive(false);
        mail.SetActive (false);
        blackLight.SetActive (false);
        GlobalControl.Instance.canMove = true;
        GlobalControl.Instance.paused = false;
    }

    public IEnumerator MailboxCutscene() {
        GlobalControl.Instance.canMove = false;
        mailboxTrigger = true;
        if (GlobalControl.Instance.cutsceneEnabled){
            speechBubble.SetActive (true);
            closingText1.SetActive (true);
            SnailSprite1.SetActive(true);
            yield return new WaitForSeconds(5f);
            closingText2.SetActive (true);
            SnailSprite1.SetActive(false);
            RecipientImage.SetActive(true);
            closingText1.SetActive (false);
            yield return new WaitForSeconds(5f);
            closingText3.SetActive (true);
            closingText2.SetActive (false);
            yield return new WaitForSeconds(5f);
            closingText3.SetActive (false);
            RecipientImage.SetActive(false);
            speechBubble.SetActive (false);
            LevelComplete.SetActive(true);
        }
        LevelComplete.SetActive(true); 
        yield return new WaitForSeconds(.25f); 
        GlobalControl.Instance.paused = true;
        mailboxTrigger = false;
    }
}
