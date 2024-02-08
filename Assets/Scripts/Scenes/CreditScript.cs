using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to boot the player from the credit screen 
//after 22 seconds. 
public class CreditScript : MonoBehaviour
{
    void Start() {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu(){
        yield return new WaitForSeconds(22f);
        SceneManager.LoadScene("Scene_Menu");
    }
}
