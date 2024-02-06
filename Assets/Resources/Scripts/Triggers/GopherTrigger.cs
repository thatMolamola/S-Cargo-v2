using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to change the animation state of the target 
//Animator if the player enters or exits a collision with it 
public class GopherTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimationController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                myAnimationController.SetBool("IsNear", true);
            }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                myAnimationController.SetBool("IsNear", false);
            }
        
    }
}
