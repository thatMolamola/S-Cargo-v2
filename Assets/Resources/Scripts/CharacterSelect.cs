using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this script is designed to update the globalController integer snailChoice
//based on the dropdown selection in the RollCharacter Scene 
public class CharacterSelect : MonoBehaviour
{
    private Animator player;
    private Animator SnailContainer;
    private Animator Door;
    private Dropdown CharacterDD;
    private GlobalControl globalController;

    public float timeLeft0;
    public bool delay;
    public bool changed;
    private int privSnailChoice;
    private Transform playerT;

    void Start()
        {
        player = GameObject.Find("SnailSprite").GetComponent<Animator>();
        playerT = GameObject.Find("SnailSprite").GetComponent<Transform>();
        Door = GameObject.Find("DoorSprite").GetComponent<Animator>();
        SnailContainer = GameObject.Find("SnailSpriteContainer").GetComponent<Animator>();
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
        //CharacterDD = GameObject.Find("CharacterSelect").GetComponent<Dropdown>();
        globalController.snailChoice = 1;
        timeLeft0 = 4;
        changed = false;
        delay = false;
    }

    public void Update() {
        if (delay) {
            timeLeft0 -= Time.deltaTime;   
            if (timeLeft0 > 2.2){
                playerT.localScale = new Vector3(-100, 100, 1);
            } 
        }
        if (timeLeft0 < 2.2) {
            if (!changed) {
                playerT.localScale = new Vector3(100, 100, 1);
                if (privSnailChoice == 1) {
                    player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/HerbertFiles/HerbertControl");
                } else if (privSnailChoice == 2) {
                    player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/LaylaFiles/Layla");
                } else if (privSnailChoice == 3) {
                    player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/FernandoFiles/Fernando");
                } else if (privSnailChoice == 4) {
                    player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/TheoFiles/Theo");
                    
                }
                changed = true;
            }
        }
        if (timeLeft0 < 1f) {
            delay = false;
            changed = false;    
            SnailContainer.SetBool("charChange", false);
            Door.SetBool("charChange", false);
            player.SetBool("RollUp", false);
            player.SetBool("ImRoll", false);
            timeLeft0 = 4;
        }
    }

    public void OnCharacterChanged (int snailChosen) {
        if (snailChosen != privSnailChoice) {
            if (changed) {
                Door.SetBool("charChange", false);
                SnailContainer.SetBool("charChange", false);
                changed = false;
            }
            privSnailChoice = snailChosen;
            globalController.snailChoice = snailChosen;
            delay = true;
            Door.SetBool("charChange", true);
            SnailContainer.SetBool("charChange", true);
            if (player.GetBool("RollUp")) {
                player.SetBool("ImRoll", true);
            }
        }
    }

    //a fun little bonus script to allow the user to click the snail on the charactee select screen
    // and have them roll up. 
    public void SnailClicked (){
        player.SetBool("RollUp", true);
    }
}
