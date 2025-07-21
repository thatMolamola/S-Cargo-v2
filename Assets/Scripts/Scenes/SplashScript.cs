using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    void Start() {
        StartCoroutine(Splash());
    }

    IEnumerator Splash() {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
