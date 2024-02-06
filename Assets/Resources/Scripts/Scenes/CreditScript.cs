using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to boot the player from the credit screen 
//after 22 seconds. 
public class CreditScript : MonoBehaviour
{
    private float timer = 22f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            SceneManager.LoadScene("Scene_Menu");
        }
    }
}
