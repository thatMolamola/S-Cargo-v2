using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to temporarily damage and immobilize the character
//upon contact with a hazard collider. 
public class HazardsTrigger : MonoBehaviour
{
    private bool immune;
    private bool inShell;
    [SerializeField] private GameObject red;
    [SerializeField] private SpriteRenderer player;
    [SerializeField] private PlayerController PC;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                if (PC.isRolling) {
                    inShell = true;
                    immune = true;
                } else {
                    inShell = false;
                }
                if (!immune) {
                    GlobalControl.Instance.canMove = false;
                    immune = true;
                    StartCoroutine(HitHazards());
                }
            }
    }

    IEnumerator HitHazards() {
        if (!inShell){
            red.SetActive(true);
            player.color = new Color(.5f,0,0,1);
            yield return new WaitForSeconds(.35f);
            red.SetActive(false);
            yield return new WaitForSeconds(1.15f);
            GlobalControl.Instance.canMove = true; 
            player.color = new Color(1,1,1,1);
            yield return new WaitForSeconds(3.5f);
            immune = false;
        }
    }
}
