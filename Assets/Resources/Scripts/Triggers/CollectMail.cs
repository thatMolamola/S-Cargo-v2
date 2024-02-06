using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to allow the player to collect mail upon 
// a collision between the player and the mail.
public class CollectMail : MonoBehaviour
{

    private Text MailCount;

    private GlobalControl globalController;

    //this MailCount is the same throughout all three levels, 
    //as all the levels have 3 pieces of mail
    private int MailCount1 = 3;


    void Start()
    {
        MailCount = GameObject.Find("Letters Collected").GetComponent<Text>();
        globalController =
                GameObject.Find("GameManager").GetComponent<GlobalControl>();
        UpdateMailCount();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            globalController.lettersCollected++;
            UpdateMailCount();
            if (globalController.lettersCollected == MailCount1) {
            globalController.allMailCollected = true;
            }
            Destroy (this.gameObject);
        }
    }

    void UpdateMailCount()
    {  
        MailCount.GetComponent<Text>().text =
            "Letters Collected: " + globalController.lettersCollected + "/" + MailCount1;
    }
}
