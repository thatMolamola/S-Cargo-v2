using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to update the player's RunTimeAnimatorController based on 
// the globalController.snailChoice value
public class PlayerSelector : MonoBehaviour
{
    private GlobalControl globalController;
    public Animator player;


    void Start(){
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
        if (globalController.snailChoice == 1) {
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/HerbertFiles/HerbertControl");
        } else if (globalController.snailChoice == 2) {
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/LaylaFiles/Layla");
        } else if (globalController.snailChoice == 3) {
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/FernandoFiles/Fernando");
        } else if (globalController.snailChoice == 4) {
            player.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites & Animations/SnailFiles/TheoFiles/Theo");
        }
    }
}
