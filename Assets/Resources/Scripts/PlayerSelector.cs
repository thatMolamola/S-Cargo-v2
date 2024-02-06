using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to update the player's RunTimeAnimatorController based on 
// the globalController.snailChoice value
public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

    void Start(){
        playerAnim.runtimeAnimatorController = GlobalControl.Instance.snailAnims[GlobalControl.Instance.snailChoice];
    }
}
