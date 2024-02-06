using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this script is designed to update the globalController integer snailChoice
//based on the selection in the RollCharacter Scene 
public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Animator playerAnim, SnailContainer, Door;
    [SerializeField] private RuntimeAnimatorController[] snailAnims;
    private bool changed = false;
    private int privSnailChoice;


    public void OnCharacterChanged (int snailChosen) {
        if (snailChosen != privSnailChoice) {
            StartCoroutine(SnailSwitch());
            if (changed) {
                Door.SetBool("charChange", false);
                SnailContainer.SetBool("charChange", false);
                changed = false;
            }
            privSnailChoice = snailChosen;
            GlobalControl.Instance.snailChoice = snailChosen;
            Door.SetBool("charChange", true);
            SnailContainer.SetBool("charChange", true);
            if (playerAnim.GetBool("RollUp")) {
                playerAnim.SetBool("ImRoll", true);
            }
        }
    }

    IEnumerator SnailSwitch() {
        playerAnim.gameObject.GetComponent<Transform>().localScale = new Vector3(-100, 100, 1);
        yield return new WaitForSeconds(1.8f);
        if (!changed) {
                playerAnim.gameObject.GetComponent<Transform>().localScale = new Vector3(100, 100, 1);
                playerAnim.runtimeAnimatorController = snailAnims[privSnailChoice - 1];
                changed = true;
        }
        yield return new WaitForSeconds(1.2f);
        changed = false;    
        SnailContainer.SetBool("charChange", false);
        Door.SetBool("charChange", false);
        playerAnim.SetBool("RollUp", false);
        playerAnim.SetBool("ImRoll", false);
    }

    //a fun little bonus script to allow the user to click the snail on the charactee select screen
    // and have them roll up. 
    public void SnailClicked (){
        playerAnim.SetBool("RollUp", true);
    }
}
