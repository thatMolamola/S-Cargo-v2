using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is designed to allow the player to collect mail upon 
// a collision between the player and the mail.
public class CollectMail : MonoBehaviour
{
    [SerializeField] private Text MailCount;

    //this MailCount is the same throughout all three levels, 
    //as all the levels have 3 pieces of mail
    private int MailCount1 = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalControl.Instance.lettersCollected++;
            UpdateMailCount();
            if (GlobalControl.Instance.lettersCollected == MailCount1) {
            GlobalControl.Instance.allMailCollected = true;
            }
            Destroy (this.gameObject);
        }
    }

    void UpdateMailCount()
    {  
        MailCount.GetComponent<Text>().text =
            ": " + GlobalControl.Instance.lettersCollected + "/" + MailCount1;
    }
}
